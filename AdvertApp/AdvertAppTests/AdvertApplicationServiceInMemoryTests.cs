using AdvertApp.Application.ApplicationServices.Contracts;
using AdvertApp.Contracts.Enums;
using AdvertApp.Contracts.Models.FormModels;
using AdvertApp.Persistance;
using AdvertAppTests.Fixtures;

namespace AdvertAppTests;

public class AdvertApplicationServiceInMemoryTests : IClassFixture<TestFixture>
{
    private readonly TestFixture _fixture;
    private readonly TestDataSeeder _seeder;

    public AdvertApplicationServiceInMemoryTests ()
    {
        _fixture = new TestFixture();
        _seeder = new TestDataSeeder();
        _seeder.SeedTestData();
    }


    [Fact]
    public async Task AddAsync_ValidValue_ResultSuccess()
    {
        var advertService = _fixture.GetService<IAdvertApplicationService>();

        var formModel = new CreateAdvertFormModel
        {
            UserId = new Guid("DB7F24C1-A46B-4BC2-A36A-B0D550F4D647"),
            Text = "Test advert text",
            Image = null
        };

        var result = advertService?.AddAsync(formModel);
        Assert.NotNull(result);
        Assert.True(result?.Result.IsSuccess);
    }

    [Fact]
    public async Task AddAsync_UserDoesNotExist_ResultFailure()
    {
        var advertService = _fixture.GetService<IAdvertApplicationService>();

        var formModel = new CreateAdvertFormModel
        {
            UserId = new Guid("DB7F24C1-A46B-4BC2-A36A-B0D550F4D649"),
            Text = "Test advert text",
            Image = null
        };

        var result = advertService?.AddAsync(formModel);
        Assert.NotNull(result);
        Assert.True(result?.Result.IsFailure);
    }

    [Fact]
    public async Task Update_AdminUserWithValidValue_ResultSuccess()
    {
        var advertService = _fixture.GetService<IAdvertApplicationService>();
        var context = _fixture.GetService<IAdvertContext>();

        var formModel = new UpdateAdvertFormModel
        {
            UserId = context.Users.Where(u => u.Role == UserRole.Admin).FirstOrDefault().Id,
            AdvertId = context.Adverts.Where(a => a.AuthorName == "User1").FirstOrDefault().Id,
            Text = "Test update advert text by admin",
            Image = null,
            Status = AdvertStatus.Active
        };

        var result = advertService?.Update(formModel);
        Assert.NotNull(result);
        Assert.True(result?.Result.IsSuccess);
    }

    [Fact]
    public async Task Update_AdvertDoesNotExist_ResultFalure()
    {
        var advertService = _fixture.GetService<IAdvertApplicationService>();
        var context = _fixture.GetService<IAdvertContext>();

        var formModel = new UpdateAdvertFormModel
        {
            UserId = context.Users.Where(u => u.Name == "User1").FirstOrDefault().Id,
            AdvertId = new Guid("DB7F24C1-A46B-4BC2-A36A-B0D550F4D649"),
            Text = "Test update advert text by other user",
            Image = null,
            Status = AdvertStatus.Active
        };

        var result = advertService?.Update(formModel);
        Assert.NotNull(result);
        Assert.True(result?.Result.IsFailure);
    }

    [Fact]
    public async Task Update_UserHasNoPermissions_ResultFalure()
    {
        var advertService = _fixture.GetService<IAdvertApplicationService>();
        var context = _fixture.GetService<IAdvertContext>();

        var formModel = new UpdateAdvertFormModel
        {
            UserId = context.Users.Where(u => u.Name == "User2").FirstOrDefault().Id,
            AdvertId = context.Adverts.Where(a => a.AuthorName == "User1").FirstOrDefault().Id,
            Text = "Test update advert text by other user",
            Image = null,
            Status = AdvertStatus.Active
        };

        var result = advertService?.Update(formModel);
        Assert.NotNull(result);
        Assert.True(result?.Result.IsFailure);
    }
}
