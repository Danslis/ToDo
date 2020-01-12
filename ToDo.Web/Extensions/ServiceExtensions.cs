using Microsoft.Extensions.DependencyInjection;
using ToDo.Services;
using ToDo.Services.Base;

namespace ToDo.Web.Extensions
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
