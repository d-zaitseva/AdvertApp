using Microsoft.Extensions.DependencyInjection;

using AdvertApp.Persistance.Repositories;
using AdvertApp.ReadWrite;
using AdvertApp.Application;

namespace AdvertApp.Persistance;

public static class Extensions
{
    public static void AddPersistance(this IServiceCollection services)
    {
        services.AddScoped<IAdvertReadRepository, AdvertRepository>();
        services.AddScoped<IAdvertWriteRepository, AdvertRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IAdvertContext, AdvertContext>();
    }
}
