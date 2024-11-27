﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Money.Data;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Money.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text")
                        .HasColumnName("claim_type");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text")
                        .HasColumnName("claim_value");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid")
                        .HasColumnName("role_id");

                    b.HasKey("Id")
                        .HasName("pk_asp_net_role_claims");

                    b.HasIndex("RoleId")
                        .HasDatabaseName("ix_asp_net_role_claims_role_id");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text")
                        .HasColumnName("claim_type");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text")
                        .HasColumnName("claim_value");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_asp_net_user_claims");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_asp_net_user_claims_user_id");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text")
                        .HasColumnName("login_provider");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text")
                        .HasColumnName("provider_key");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text")
                        .HasColumnName("provider_display_name");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("LoginProvider", "ProviderKey")
                        .HasName("pk_asp_net_user_logins");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_asp_net_user_logins_user_id");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid")
                        .HasColumnName("role_id");

                    b.HasKey("UserId", "RoleId")
                        .HasName("pk_asp_net_user_roles");

                    b.HasIndex("RoleId")
                        .HasDatabaseName("ix_asp_net_user_roles_role_id");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text")
                        .HasColumnName("login_provider");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Value")
                        .HasColumnType("text")
                        .HasColumnName("value");

                    b.HasKey("UserId", "LoginProvider", "Name")
                        .HasName("pk_asp_net_user_tokens");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Money.Data.Entities.ApplicationRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text")
                        .HasColumnName("concurrency_stamp");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("name");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("normalized_name");

                    b.HasKey("Id")
                        .HasName("pk_asp_net_roles");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Money.Data.Entities.ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer")
                        .HasColumnName("access_failed_count");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text")
                        .HasColumnName("concurrency_stamp");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("email");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean")
                        .HasColumnName("email_confirmed");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean")
                        .HasColumnName("lockout_enabled");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("lockout_end");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("normalized_email");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("normalized_user_name");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text")
                        .HasColumnName("password_hash");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text")
                        .HasColumnName("phone_number");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean")
                        .HasColumnName("phone_number_confirmed");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text")
                        .HasColumnName("security_stamp");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean")
                        .HasColumnName("two_factor_enabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("user_name");

                    b.HasKey("Id")
                        .HasName("pk_asp_net_users");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Money.Data.Entities.Category", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id")
                        .HasColumnOrder(1);

                    b.Property<int>("Id")
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasColumnOrder(2);

                    b.Property<string>("Color")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("color");

                    b.Property<string>("Description")
                        .HasMaxLength(4000)
                        .HasColumnType("character varying(4000)")
                        .HasColumnName("description");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("name");

                    b.Property<int?>("Order")
                        .HasColumnType("integer")
                        .HasColumnName("order");

                    b.Property<int?>("ParentId")
                        .HasColumnType("integer")
                        .HasColumnName("parent_id");

                    b.Property<int>("TypeId")
                        .HasColumnType("integer")
                        .HasColumnName("type_id");

                    b.HasKey("UserId", "Id")
                        .HasName("pk_categories");

                    b.HasIndex("UserId", "ParentId")
                        .HasDatabaseName("ix_categories_user_id_parent_id");

                    b.ToTable("categories", (string)null);
                });

            modelBuilder.Entity("Money.Data.Entities.DomainUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<Guid>("AuthUserId")
                        .HasColumnType("uuid")
                        .HasColumnName("auth_user_id");

                    b.Property<int>("NextCategoryId")
                        .HasColumnType("integer")
                        .HasColumnName("next_category_id");

                    b.Property<int>("NextOperationId")
                        .HasColumnType("integer")
                        .HasColumnName("next_operation_id");

                    b.Property<int>("NextPlaceId")
                        .HasColumnType("integer")
                        .HasColumnName("next_place_id");

                    b.HasKey("Id")
                        .HasName("pk_domain_users");

                    b.ToTable("domain_users", (string)null);
                });

            modelBuilder.Entity("Money.Data.Entities.FastOperation", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id")
                        .HasColumnOrder(1);

                    b.Property<int>("Id")
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasColumnOrder(2);

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer")
                        .HasColumnName("category_id");

                    b.Property<string>("Comment")
                        .HasMaxLength(4000)
                        .HasColumnType("character varying(4000)")
                        .HasColumnName("comment");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("name");

                    b.Property<int?>("Order")
                        .HasColumnType("integer")
                        .HasColumnName("order");

                    b.Property<int?>("PlaceId")
                        .HasColumnType("integer")
                        .HasColumnName("place_id");

                    b.Property<decimal>("Sum")
                        .HasColumnType("numeric")
                        .HasColumnName("sum");

                    b.HasKey("UserId", "Id")
                        .HasName("pk_fast_operations");

                    b.ToTable("fast_operations", (string)null);
                });

            modelBuilder.Entity("Money.Data.Entities.Operation", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id")
                        .HasColumnOrder(1);

                    b.Property<int>("Id")
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasColumnOrder(2);

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer")
                        .HasColumnName("category_id");

                    b.Property<string>("Comment")
                        .HasMaxLength(4000)
                        .HasColumnType("character varying(4000)")
                        .HasColumnName("comment");

                    b.Property<int?>("CreatedTaskId")
                        .HasColumnType("integer")
                        .HasColumnName("created_task_id");

                    b.Property<DateTime>("Date")
                        .HasColumnType("date")
                        .HasColumnName("date");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<int?>("PlaceId")
                        .HasColumnType("integer")
                        .HasColumnName("place_id");

                    b.Property<decimal>("Sum")
                        .HasColumnType("numeric")
                        .HasColumnName("sum");

                    b.HasKey("UserId", "Id")
                        .HasName("pk_operations");

                    b.ToTable("operations", (string)null);
                });

            modelBuilder.Entity("Money.Data.Entities.Place", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id")
                        .HasColumnOrder(1);

                    b.Property<int>("Id")
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasColumnOrder(2);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<DateTime>("LastUsedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_used_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("name");

                    b.HasKey("UserId", "Id")
                        .HasName("pk_places");

                    b.ToTable("places", (string)null);
                });

            modelBuilder.Entity("Money.Data.Entities.RegularOperation", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id")
                        .HasColumnOrder(1);

                    b.Property<int>("Id")
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasColumnOrder(2);

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer")
                        .HasColumnName("category_id");

                    b.Property<string>("Comment")
                        .HasMaxLength(4000)
                        .HasColumnType("character varying(4000)")
                        .HasColumnName("comment");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("name");

                    b.Property<int?>("PlaceId")
                        .HasColumnType("integer")
                        .HasColumnName("place_id");

                    b.Property<decimal>("Sum")
                        .HasColumnType("numeric")
                        .HasColumnName("sum");

                    b.HasKey("UserId", "Id")
                        .HasName("pk_regular_operations");

                    b.ToTable("regular_operations", (string)null);
                });

            modelBuilder.Entity("OpenIddict.EntityFrameworkCore.Models.OpenIddictEntityFrameworkCoreApplication", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<string>("ApplicationType")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("application_type");

                    b.Property<string>("ClientId")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("client_id");

                    b.Property<string>("ClientSecret")
                        .HasColumnType("text")
                        .HasColumnName("client_secret");

                    b.Property<string>("ClientType")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("client_type");

                    b.Property<string>("ConcurrencyToken")
                        .IsConcurrencyToken()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("concurrency_token");

                    b.Property<string>("ConsentType")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("consent_type");

                    b.Property<string>("DisplayName")
                        .HasColumnType("text")
                        .HasColumnName("display_name");

                    b.Property<string>("DisplayNames")
                        .HasColumnType("text")
                        .HasColumnName("display_names");

                    b.Property<string>("JsonWebKeySet")
                        .HasColumnType("text")
                        .HasColumnName("json_web_key_set");

                    b.Property<string>("Permissions")
                        .HasColumnType("text")
                        .HasColumnName("permissions");

                    b.Property<string>("PostLogoutRedirectUris")
                        .HasColumnType("text")
                        .HasColumnName("post_logout_redirect_uris");

                    b.Property<string>("Properties")
                        .HasColumnType("text")
                        .HasColumnName("properties");

                    b.Property<string>("RedirectUris")
                        .HasColumnType("text")
                        .HasColumnName("redirect_uris");

                    b.Property<string>("Requirements")
                        .HasColumnType("text")
                        .HasColumnName("requirements");

                    b.Property<string>("Settings")
                        .HasColumnType("text")
                        .HasColumnName("settings");

                    b.HasKey("Id")
                        .HasName("pk_open_iddict_applications");

                    b.HasIndex("ClientId")
                        .IsUnique()
                        .HasDatabaseName("ix_open_iddict_applications_client_id");

                    b.ToTable("OpenIddictApplications", (string)null);
                });

            modelBuilder.Entity("OpenIddict.EntityFrameworkCore.Models.OpenIddictEntityFrameworkCoreAuthorization", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<string>("ApplicationId")
                        .HasColumnType("text")
                        .HasColumnName("application_id");

                    b.Property<string>("ConcurrencyToken")
                        .IsConcurrencyToken()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("concurrency_token");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("creation_date");

                    b.Property<string>("Properties")
                        .HasColumnType("text")
                        .HasColumnName("properties");

                    b.Property<string>("Scopes")
                        .HasColumnType("text")
                        .HasColumnName("scopes");

                    b.Property<string>("Status")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("status");

                    b.Property<string>("Subject")
                        .HasMaxLength(400)
                        .HasColumnType("character varying(400)")
                        .HasColumnName("subject");

                    b.Property<string>("Type")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("type");

                    b.HasKey("Id")
                        .HasName("pk_open_iddict_authorizations");

                    b.HasIndex("ApplicationId", "Status", "Subject", "Type")
                        .HasDatabaseName("ix_open_iddict_authorizations_application_id_status_subject_type");

                    b.ToTable("OpenIddictAuthorizations", (string)null);
                });

            modelBuilder.Entity("OpenIddict.EntityFrameworkCore.Models.OpenIddictEntityFrameworkCoreScope", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<string>("ConcurrencyToken")
                        .IsConcurrencyToken()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("concurrency_token");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Descriptions")
                        .HasColumnType("text")
                        .HasColumnName("descriptions");

                    b.Property<string>("DisplayName")
                        .HasColumnType("text")
                        .HasColumnName("display_name");

                    b.Property<string>("DisplayNames")
                        .HasColumnType("text")
                        .HasColumnName("display_names");

                    b.Property<string>("Name")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("name");

                    b.Property<string>("Properties")
                        .HasColumnType("text")
                        .HasColumnName("properties");

                    b.Property<string>("Resources")
                        .HasColumnType("text")
                        .HasColumnName("resources");

                    b.HasKey("Id")
                        .HasName("pk_open_iddict_scopes");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasDatabaseName("ix_open_iddict_scopes_name");

                    b.ToTable("OpenIddictScopes", (string)null);
                });

            modelBuilder.Entity("OpenIddict.EntityFrameworkCore.Models.OpenIddictEntityFrameworkCoreToken", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<string>("ApplicationId")
                        .HasColumnType("text")
                        .HasColumnName("application_id");

                    b.Property<string>("AuthorizationId")
                        .HasColumnType("text")
                        .HasColumnName("authorization_id");

                    b.Property<string>("ConcurrencyToken")
                        .IsConcurrencyToken()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("concurrency_token");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("creation_date");

                    b.Property<DateTime?>("ExpirationDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("expiration_date");

                    b.Property<string>("Payload")
                        .HasColumnType("text")
                        .HasColumnName("payload");

                    b.Property<string>("Properties")
                        .HasColumnType("text")
                        .HasColumnName("properties");

                    b.Property<DateTime?>("RedemptionDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("redemption_date");

                    b.Property<string>("ReferenceId")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("reference_id");

                    b.Property<string>("Status")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("status");

                    b.Property<string>("Subject")
                        .HasMaxLength(400)
                        .HasColumnType("character varying(400)")
                        .HasColumnName("subject");

                    b.Property<string>("Type")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("type");

                    b.HasKey("Id")
                        .HasName("pk_open_iddict_tokens");

                    b.HasIndex("AuthorizationId")
                        .HasDatabaseName("ix_open_iddict_tokens_authorization_id");

                    b.HasIndex("ReferenceId")
                        .IsUnique()
                        .HasDatabaseName("ix_open_iddict_tokens_reference_id");

                    b.HasIndex("ApplicationId", "Status", "Subject", "Type")
                        .HasDatabaseName("ix_open_iddict_tokens_application_id_status_subject_type");

                    b.ToTable("OpenIddictTokens", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Money.Data.Entities.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_asp_net_role_claims_asp_net_roles_role_id");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("Money.Data.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_asp_net_user_claims_asp_net_users_user_id");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("Money.Data.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_asp_net_user_logins_asp_net_users_user_id");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("Money.Data.Entities.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_asp_net_user_roles_asp_net_roles_role_id");

                    b.HasOne("Money.Data.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_asp_net_user_roles_asp_net_users_user_id");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("Money.Data.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_asp_net_user_tokens_asp_net_users_user_id");
                });

            modelBuilder.Entity("Money.Data.Entities.Category", b =>
                {
                    b.HasOne("Money.Data.Entities.DomainUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_categories_domain_users_user_id");

                    b.HasOne("Money.Data.Entities.Category", "ParentCategory")
                        .WithMany("SubCategories")
                        .HasForeignKey("UserId", "ParentId")
                        .HasConstraintName("fk_categories_categories_user_id_parent_id");

                    b.Navigation("ParentCategory");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Money.Data.Entities.FastOperation", b =>
                {
                    b.HasOne("Money.Data.Entities.DomainUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_fast_operations_domain_users_user_id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Money.Data.Entities.Operation", b =>
                {
                    b.HasOne("Money.Data.Entities.DomainUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_operations_domain_users_user_id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Money.Data.Entities.Place", b =>
                {
                    b.HasOne("Money.Data.Entities.DomainUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_places_domain_users_user_id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Money.Data.Entities.RegularOperation", b =>
                {
                    b.HasOne("Money.Data.Entities.DomainUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_regular_operations_domain_users_user_id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("OpenIddict.EntityFrameworkCore.Models.OpenIddictEntityFrameworkCoreAuthorization", b =>
                {
                    b.HasOne("OpenIddict.EntityFrameworkCore.Models.OpenIddictEntityFrameworkCoreApplication", "Application")
                        .WithMany("Authorizations")
                        .HasForeignKey("ApplicationId")
                        .HasConstraintName("fk_open_iddict_authorizations_open_iddict_applications_application");

                    b.Navigation("Application");
                });

            modelBuilder.Entity("OpenIddict.EntityFrameworkCore.Models.OpenIddictEntityFrameworkCoreToken", b =>
                {
                    b.HasOne("OpenIddict.EntityFrameworkCore.Models.OpenIddictEntityFrameworkCoreApplication", "Application")
                        .WithMany("Tokens")
                        .HasForeignKey("ApplicationId")
                        .HasConstraintName("fk_open_iddict_tokens_open_iddict_applications_application_id");

                    b.HasOne("OpenIddict.EntityFrameworkCore.Models.OpenIddictEntityFrameworkCoreAuthorization", "Authorization")
                        .WithMany("Tokens")
                        .HasForeignKey("AuthorizationId")
                        .HasConstraintName("fk_open_iddict_tokens_open_iddict_authorizations_authorization_id");

                    b.Navigation("Application");

                    b.Navigation("Authorization");
                });

            modelBuilder.Entity("Money.Data.Entities.Category", b =>
                {
                    b.Navigation("SubCategories");
                });

            modelBuilder.Entity("OpenIddict.EntityFrameworkCore.Models.OpenIddictEntityFrameworkCoreApplication", b =>
                {
                    b.Navigation("Authorizations");

                    b.Navigation("Tokens");
                });

            modelBuilder.Entity("OpenIddict.EntityFrameworkCore.Models.OpenIddictEntityFrameworkCoreAuthorization", b =>
                {
                    b.Navigation("Tokens");
                });
#pragma warning restore 612, 618
        }
    }
}
