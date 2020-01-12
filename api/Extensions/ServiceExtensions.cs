using DAL.Service;
using DAL.Service.Base;
using Microsoft.Extensions.DependencyInjection;

namespace api.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<ICardService, CardService>();
            return services;
        }
    }
}
