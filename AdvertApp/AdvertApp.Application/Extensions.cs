using Microsoft.Extensions.DependencyInjection;
using AdvertApp.Application.ApplicationServices;
using AdvertApp.Application.ApplicationServices.Contracts;

namespace AdvertApp.Applicationж
{
    public static class Extensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IAdvertApplicationService, AdvertApplicationService>();
            services.AddScoped<IImageApplicationService, ImageApplicationService>();
        }
    }
}
