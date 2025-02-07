//go:debug x509negativeserial=1
package main

import (
	"database/sql"
	"fmt"
	_ "github.com/denisenkom/go-mssqldb"
	"github.com/jmoiron/sqlx"
	_ "github.com/lib/pq"
	"log"
	"os"
	"strings"
	"time"
)

type config struct {
	host     string
	port     int
	user     string
	password string
	dbName   string
	provider string
}

func main() {
	newConfig := config{
		host:     "localhost",
		port:     5432,
		user:     "postgres",
		password: "RjirfLeyz",
		dbName:   "money-dev3",
		provider: "postgres",
	}

	oldConfig := config{
		host:     "localhost",
		port:     1433,
		user:     "money",
		password: "money",
		dbName:   "money-dev",
		provider: "sqlserver",
	}

	oldDatabaseConnectionString := fmt.Sprintf("Data Source=%s,%d;Initial Catalog=%s;"+
		"Persist Security Info=True;User ID=%s;Password=%s;TrustServerCertificate=True;",
		oldConfig.host, oldConfig.port, oldConfig.dbName, oldConfig.user, oldConfig.password)

	newDatabaseConnectionString := fmt.Sprintf("host=%s port=%d user=%s "+
		"password=%s dbname=%s sslmode=disable",
		newConfig.host, newConfig.port, newConfig.user, newConfig.password, newConfig.dbName)

	oldDatabase, err := sqlx.Connect(oldConfig.provider, oldDatabaseConnectionString)
	if err != nil {
		log.Fatalln("Old connect\n", err)
	}

	newDatabase, err := sqlx.Connect(newConfig.provider, newDatabaseConnectionString)
	if err != nil {
		log.Fatalln("New connect\n", err)
	}

	prepareDatabases(oldDatabase)
	truncateDatabase(newDatabase)

	transporter := CreateTransporter()

	processTable(newDatabase, oldDatabase, &transporter.AuthUser)
	processTable(newDatabase, oldDatabase, &transporter.DomainUser)
	processTable(newDatabase, oldDatabase, &transporter.DebtOwner)
	processTable(newDatabase, oldDatabase, &transporter.Debt)
	processTable(newDatabase, oldDatabase, &transporter.Category)
	processTable(newDatabase, oldDatabase, &transporter.Operation)
	processTable(newDatabase, oldDatabase, &transporter.FastOperation)
	processTable(newDatabase, oldDatabase, &transporter.RegularOperation)
	processTable(newDatabase, oldDatabase, &transporter.Place)

	resetDatabase(oldDatabase)
}

func truncateDatabase(newDatabase *sqlx.DB) sql.Result {
	return newDatabase.MustExec(`
TRUNCATE "AspNetUsers" CASCADE;
TRUNCATE "domain_users" CASCADE;
TRUNCATE "debt_owners" CASCADE;
TRUNCATE "debts" CASCADE;
TRUNCATE "categories" CASCADE;
TRUNCATE "operations" CASCADE;
TRUNCATE "fast_operations" CASCADE;
TRUNCATE "places" CASCADE;
TRUNCATE "regular_operations" CASCADE;
`)
}

func prepareDatabases(oldDatabase *sqlx.DB) {
	tx, err := oldDatabase.Beginx()
	if err != nil {
		log.Fatalln("Transaction start failed:\n", err)
	}
	defer func() {
		if p := recover(); p != nil {
			_ = tx.Rollback()
			log.Fatalln("Panic occurred:\n", p)
		}
	}()

	tx.MustExec(`
ALTER TABLE [System].[User]
ADD Guid uniqueidentifier
`)

	tx.MustExec(`
UPDATE [System].[User]
SET Guid = NEWID()
`)

	tx.MustExec(`
ALTER TABLE Money.RegularTask
    ADD Sum decimal(18, 2), CategoryId int, Comment nvarchar(4000), PlaceId int
`)

	tx.MustExec(`
UPDATE Money.RegularTask
SET Sum        = p.Sum,
    CategoryId = p.CategoryId,
    Comment    = p.Comment,
    PlaceId    = p.PlaceId
FROM Money.RegularTask r
         JOIN Money.Payment p ON r.TaskId = p.TaskId
`)

	if err := tx.Commit(); err != nil {
		log.Fatalln("Commit failed:\n", err)
	}
}

func resetDatabase(oldDatabase *sqlx.DB) {
	tx, err := oldDatabase.Beginx()
	if err != nil {
		log.Fatalln("Transaction start failed:\n", err)
	}
	defer func() {
		if p := recover(); p != nil {
			_ = tx.Rollback()
			log.Fatalln("Panic occurred:\n", p)
		}
	}()

	tx.MustExec(`
ALTER TABLE Money.RegularTask
DROP COLUMN Sum, CategoryId, Comment, PlaceId
`)
	tx.MustExec(`
ALTER TABLE [System].[User]
DROP COLUMN Guid
`)

	if err := tx.Commit(); err != nil {
		log.Fatalln("Commit failed:\n", err)
	}
}

func processTable[O any, N any](newDatabase *sqlx.DB, oldDatabase *sqlx.DB, table TableMapping[O, N]) {
	batchSize := 1000
	totalRows := 0
	startTime := time.Now()
	logger := log.New(os.Stdout, "", log.Lshortfile)

	baseTable := table.GetBaseTable()
	oldColumns, _ := baseTable.GetEscapedColumnNames()
	newColumns, _ := baseTable.GetNewColumnNames()
	insertColumns, _ := baseTable.GetInsertColumnNames()

	logger.Printf("Starting processing table %s -> %s", baseTable.OldName, baseTable.NewName)

	tx, err := newDatabase.Beginx()
	if err != nil {
		logger.Fatalf("Transaction start failed: %v", err)
	}
	defer func() {
		if p := recover(); p != nil {
			logger.Printf("Panic occurred: %v. Rolling back", p)
			_ = tx.Rollback()
			panic(p)
		}
	}()

	insertQuery := fmt.Sprintf(
		"INSERT INTO %s (%s) VALUES(%s)",
		baseTable.NewName,
		newColumns,
		insertColumns,
	)

	offset := 0
	batchCount := 0
	for {
		batchStart := time.Now()
		batchCount++

		selectQuery := fmt.Sprintf(
			"SELECT %s FROM %s ORDER BY 1 OFFSET %d ROWS FETCH NEXT %d ROWS ONLY",
			oldColumns,
			baseTable.OldName,
			offset,
			batchSize,
		)

		var oldBatch []O
		if err := oldDatabase.Select(&oldBatch, selectQuery); err != nil {
			_ = tx.Rollback()
			logger.Fatalf("Batch %d read failed: %v", batchCount, err)
		}

		if len(oldBatch) == 0 {
			logger.Printf("No more rows. Total batches: %d", batchCount-1)
			break
		}

		newBatch := make([]N, 0, len(oldBatch))
		for _, oldRow := range oldBatch {
			newBatch = append(newBatch, table.Transform(oldRow))
		}

		if _, err := tx.NamedExec(insertQuery, newBatch); err != nil {
			_ = tx.Rollback()
			logger.Fatalf("Batch %d insert failed: %v", batchCount, err)
		}

		processed := len(oldBatch)
		totalRows += processed
		offset += processed
		logger.Printf("Batch %d: Completed %d rows (total %d) in %v",
			batchCount, processed, totalRows, time.Since(batchStart))
	}

	if err := tx.Commit(); err != nil {
		logger.Fatalf("Commit failed: %v", err)
	}

	totalTime := time.Since(startTime)
	logger.Printf("Completed %s -> %s", baseTable.OldName, baseTable.NewName)
	logger.Printf("Total rows: %d | Batches: %d | Total time: %v",
		totalRows, batchCount-1, totalTime.Round(time.Millisecond))
	logger.Printf("Average speed: %.1f rows/sec", float64(totalRows)/totalTime.Seconds())
	logger.Printf(strings.Repeat("-", 30))
}
