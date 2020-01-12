using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using ToDo.DataAccess.Base;
using ToDo.DataAccess.Repositories;

namespace ToDo.Web.Extensions
{
    public static class RepositoriesExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddHttpClient<ICardRepository, CardRepository>(c =>
            {
                c.BaseAddress = new Uri("https://localhost:5001");
            }).ConfigurePrimaryHttpMessageHandler(() =>
            {
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };
                return handler;
            });
            return services;
        }
    }
}
