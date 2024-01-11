using AdvertApp.Application.ApplicationServices;
using AdvertApp.Application.ApplicationServices.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace AdvertApp.Application;

public static class Extensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAdvertApplicationService, AdvertApplicationService>();
        services.AddScoped<IImageApplicationService, ImageApplicationService>();
    }
}
