using AdvertApp.Application.ApplicationServices.Contracts;
using AdvertApp.Contracts.Models.FormModels;
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

        var result = advertService.AddAsync(formModel);
        Assert.NotNull(result);
        Assert.True(result.Result.IsSuccess);
    }
}
