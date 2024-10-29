﻿using Money.Api.Tests.TestTools;
using Money.Api.Tests.TestTools.Entities;
using Money.ApiClient;
using Money.Data.Entities;
using Money.Data.Extensions;

namespace Money.Api.Tests.Payments;

public class PaymentPlaceTests
{
    private DatabaseClient _dbClient;
    private TestUser _user;
    private MoneyClient _apiClient;

    [SetUp]
    public void Setup()
    {
        _dbClient = Integration.GetDatabaseClient();
        _user = _dbClient.WithUser();
        _apiClient = new MoneyClient(Integration.GetHttpClient(), Console.WriteLine);
        _apiClient.SetUser(_user);
    }

    /// <summary>
    ///     Создали платёж с оригинальным местом и место появилось.
    /// </summary>
    [Test]
    public async Task CreatePlaceTest()
    {
        TestCategory category = _user.WithCategory();
        _dbClient.Save();

        PaymentClient.SaveRequest request = new()
        {
            CategoryId = category.Id,
            Sum = 1,
            Date = DateTime.Now,
            Place = "place1",
        };

        await _apiClient.Payment.Create(request).IsSuccess();

        DomainPlace[] dbPlaces = _dbClient.CreateApplicationDbContext()
            .Places
            .IsUserEntity(_user.Id)
            .ToArray();

        Assert.That(dbPlaces, Has.Length.EqualTo(1));

        Assert.Multiple(() =>
        {
            Assert.That(dbPlaces[0].Name, Is.EqualTo(request.Place));
            Assert.That(dbPlaces[0].Id, Is.EqualTo(1));
            Assert.That(dbPlaces[0].IsDeleted, Is.False);
        });
    }

    /// <summary>
    ///     Занулили место у единственного платежа, место удалилось.
    /// </summary>
    [Test]
    [TestCase("")]
    [TestCase(null)]
    public async Task RemovePlaceAfterSetPaymentZeroPlaceTest(string? updatedPlace)
    {
        TestCategory category = _user.WithCategory();
        _dbClient.Save();

        PaymentClient.SaveRequest request = new()
        {
            CategoryId = category.Id,
            Sum = 1,
            Date = DateTime.Now,
            Place = "place1",
        };

        int paymentId = await _apiClient.Payment.Create(request).IsSuccessWithContent();
        request.Place = updatedPlace;
        await _apiClient.Payment.Update(paymentId, request).IsSuccess();

        DomainPlace[] dbPlaces = _dbClient.CreateApplicationDbContext()
            .Places
            .Where(x => x.UserId == _user.Id)
            .ToArray();

        Assert.Multiple(() =>
        {
            Assert.That(dbPlaces, Has.Length.EqualTo(1));
            Assert.That(dbPlaces[0].IsDeleted, Is.True);
        });
    }

    /// <summary>
    ///     Два платежа имеют одно место, и если место занулить у одного из платежей, оно не удалится.
    /// </summary>
    [Test]
    public async Task DontRemovePlaceAfterSetPaymentZeroPlaceTest()
    {
        TestCategory category = _user.WithCategory();
        _dbClient.Save();

        PaymentClient.SaveRequest request = new()
        {
            CategoryId = category.Id,
            Sum = 1,
            Date = DateTime.Now,
            Place = "place1",
        };

        int paymentId1 = await _apiClient.Payment.Create(request).IsSuccessWithContent();
        int paymentId2 = await _apiClient.Payment.Create(request).IsSuccessWithContent();

        request.Place = null;
        await _apiClient.Payment.Update(paymentId2, request).IsSuccess();

        DomainPlace[] dbPlaces = _dbClient.CreateApplicationDbContext()
            .Places
            .Where(x => x.UserId == _user.Id)
            .ToArray();

        Assert.Multiple(() =>
        {
            Assert.That(dbPlaces, Has.Length.EqualTo(1));
            Assert.That(dbPlaces[0].IsDeleted, Is.False);
        });
    }

    /// <summary>
    ///     Два платежа имеют одно место, после зануления места всех платежей, место исчезло.
    /// </summary>
    [Test]
    public async Task RemovePlaceAfterSetAllPaymentsZeroPlaceTest()
    {
        TestCategory category = _user.WithCategory();
        _dbClient.Save();

        PaymentClient.SaveRequest request = new()
        {
            CategoryId = category.Id,
            Sum = 1,
            Date = DateTime.Now,
            Place = "place1",
        };

        int paymentId1 = await _apiClient.Payment.Create(request).IsSuccessWithContent();
        int paymentId2 = await _apiClient.Payment.Create(request).IsSuccessWithContent();

        request.Place = null;
        await _apiClient.Payment.Update(paymentId1, request).IsSuccess();
        await _apiClient.Payment.Update(paymentId2, request).IsSuccess();

        DomainPlace[] dbPlaces = _dbClient.CreateApplicationDbContext()
            .Places
            .Where(x => x.UserId == _user.Id)
            .ToArray();

        Assert.Multiple(() =>
        {
            Assert.That(dbPlaces, Has.Length.EqualTo(1));
            Assert.That(dbPlaces[0].IsDeleted, Is.True);
        });
    }

    /// <summary>
    ///     Один платёж имеет уникальное место.
    ///     После удаления платежа, оно удалится.
    /// </summary>
    [Test]
    public async Task DeletePlaceAfterDeletePaymentTest()
    {
        TestCategory category = _user.WithCategory();
        _dbClient.Save();

        PaymentClient.SaveRequest request = new()
        {
            CategoryId = category.Id,
            Sum = 1,
            Date = DateTime.Now,
            Place = "place1",
        };

        int paymentId = await _apiClient.Payment.Create(request).IsSuccessWithContent();
        await _apiClient.Payment.Delete(paymentId).IsSuccess();

        DomainPlace[] dbPlaces = _dbClient.CreateApplicationDbContext()
            .Places
            .Where(x => x.UserId == _user.Id)
            .ToArray();

        Assert.Multiple(() =>
        {
            Assert.That(dbPlaces, Has.Length.EqualTo(1));
            Assert.That(dbPlaces[0].IsDeleted, Is.True);
        });
    }

    /// <summary>
    ///     Один платёж имеет уникальное место.
    ///     После удаления платежа, оно удалится.
    ///     После восстановления платежа должно восстановиться.
    /// </summary>
    [Test]
    public async Task RestorePlaceAfterRestorePaymentTest()
    {
        TestCategory category = _user.WithCategory();
        _dbClient.Save();

        PaymentClient.SaveRequest request = new()
        {
            CategoryId = category.Id,
            Sum = 1,
            Date = DateTime.Now,
            Place = "place1",
        };

        int paymentId = await _apiClient.Payment.Create(request).IsSuccessWithContent();
        await _apiClient.Payment.Delete(paymentId).IsSuccess();
        await _apiClient.Payment.Restore(paymentId).IsSuccess();

        DomainPlace[] dbPlaces = _dbClient.CreateApplicationDbContext()
            .Places
            .Where(x => x.UserId == _user.Id)
            .ToArray();

        Assert.Multiple(() =>
        {
            Assert.That(dbPlaces, Has.Length.EqualTo(1));
            Assert.That(dbPlaces[0].IsDeleted, Is.False);
        });
    }

    /// <summary>
    ///     Один платёж имеет уникальное место.
    ///     После удаления платежа, оно удалится.
    ///     После восстановления платежа должно
    ///     восстановиться.
    /// </summary>
    [Test]
    public async Task RestorePlaceAfterCreatePaymentWithDeletedPlaceTest()
    {
        TestCategory category = _user.WithCategory();
        _dbClient.Save();

        PaymentClient.SaveRequest request = new()
        {
            CategoryId = category.Id,
            Sum = 1,
            Date = DateTime.Now,
            Place = "place1",
        };

        int paymentId1 = await _apiClient.Payment.Create(request).IsSuccessWithContent();
        await _apiClient.Payment.Delete(paymentId1).IsSuccess();
        int paymentId2 = await _apiClient.Payment.Create(request).IsSuccessWithContent();

        DomainPlace[] dbPlaces = _dbClient.CreateApplicationDbContext()
            .Places
            .Where(x => x.UserId == _user.Id)
            .ToArray();

        Assert.Multiple(() =>
        {
            Assert.That(dbPlaces, Has.Length.EqualTo(1));
            Assert.That(dbPlaces[0].IsDeleted, Is.False);
        });
    }

    /// <summary>
    ///     Создадим три плейса и проверим параметры offset и count.
    /// </summary>
    [Test]
    public async Task GetPlacesOffsetAndCountTest()
    {
        TestCategory category = _user.WithCategory();
        TestPlace place = _user.WithPlace();
        _user.WithPlace();
        _user.WithPlace();
        _dbClient.Save();

        string[]? apiPlaces = await _apiClient.Payment.GetPlaces(0, 1, place.Name.Substring(0, 1)).IsSuccessWithContent();
        Assert.That(apiPlaces, Is.Not.Null);
        Assert.That(apiPlaces, Has.Length.EqualTo(1));

        apiPlaces = await _apiClient.Payment.GetPlaces(1, 10, place.Name.Substring(0, 1)).IsSuccessWithContent();
        Assert.That(apiPlaces, Is.Not.Null);
        Assert.That(apiPlaces, Has.Length.EqualTo(2));

        apiPlaces = await _apiClient.Payment.GetPlaces(2, 10, place.Name.Substring(0, 1)).IsSuccessWithContent();
        Assert.That(apiPlaces, Is.Not.Null);
        Assert.That(apiPlaces, Has.Length.EqualTo(1));
    }

    /// <summary>
    ///     Создадим три плейса и проверим параметры offset и count.
    /// </summary>
    [Test]
    public async Task GetPlacesWithEmptySearchTest()
    {
        TestCategory category = _user.WithCategory();
        _user.WithPlace();
        _dbClient.Save();

        string[]? apiPlaces = await _apiClient.Payment.GetPlaces(0, 1).IsSuccessWithContent();
        Assert.That(apiPlaces, Is.Not.Null);
        Assert.That(apiPlaces, Has.Length.EqualTo(1));

        apiPlaces = await _apiClient.Payment.GetPlaces(0, 1, "").IsSuccessWithContent();
        Assert.That(apiPlaces, Is.Not.Null);
        Assert.That(apiPlaces, Has.Length.EqualTo(1));
    }

    /// <summary>
    ///     Если фильтр пустой, на первой позиции будем последнее используемое место.
    /// </summary>
    [Test]
    public async Task GetPlacesOrderByLastUsedDateTest()
    {
        TestCategory category = _user.WithCategory();
        TestPlace place3 = _user.WithPlace().SetLastUsedDate(DateTime.Now.AddMinutes(-2));
        TestPlace place2 = _user.WithPlace().SetLastUsedDate(DateTime.Now.AddMinutes(-1));
        TestPlace place1 = _user.WithPlace().SetLastUsedDate(DateTime.Now);
        _dbClient.Save();

        string[]? apiPlaces = await _apiClient.Payment.GetPlaces(0, 100).IsSuccessWithContent();
        Assert.That(apiPlaces, Is.Not.Null);
        Assert.That(apiPlaces, Has.Length.EqualTo(3));

        Assert.Multiple(() =>
        {
            Assert.That(apiPlaces[0], Is.EqualTo(place1.Name));
            Assert.That(apiPlaces[2], Is.EqualTo(place3.Name));
        });
    }
}
