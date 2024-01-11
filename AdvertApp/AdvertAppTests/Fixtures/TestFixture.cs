using AdvertApp.AutoMapping;
using AdvertApp.Application;
using AdvertApp.Persistance;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AdvertAppTests.Fixtures;

public class TestFixture : TestBedFixture
{
    protected override void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        base.AddServices(services, configuration);

        services.AddPersistanceInMemory();    
        services.AddApplication();
        services.AddAutoMapper(typeof(AppMappingProfile));
    }

    protected override ValueTask DisposeAsyncCore()
        => new();
}
