using List_of_products.Presentation.Interfaces;
using List_of_products.Presentation.Mappers;
using List_of_products.Presentation.Configuration;
using List_of_products.Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;

namespace List_of_products.Presentation
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IProductArticleMapper, ProductArticleMapper>();

            var apiSettings = configuration.GetSection("ApiSettings").Get<ApiSettings>()
                ?? throw new InvalidOperationException("ApiSettings configuration is missing");

            services.AddHttpClient("ApiClient", client =>
            {
                client.BaseAddress = new Uri(apiSettings.BaseUrl);
            })
            .AddStandardResiliencePolicies();

            services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("ApiClient"));

            return services;
        }
    }
}
