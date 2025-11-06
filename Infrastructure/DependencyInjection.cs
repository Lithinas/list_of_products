using List_of_products.Application.Interfaces;
using List_of_products.Infrastructure.Repositories;
using List_of_products.Infrastructure.Mappers;
using List_of_products.Infrastructure.Services;
using List_of_products.Infrastructure.Extensions;
using List_of_products.Infrastructure.Configuration;

namespace List_of_products.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Bind ExternalServicesSettings from configuration
            services.Configure<ExternalServicesSettings>(
                configuration.GetSection("ExternalServices"));

            // Configure named HttpClient for external data source with Polly resilience policies
            services.AddHttpClient("ProductDataClient")
                .AddStandardResiliencePolicies();

            // Register infrastructure services
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductMapper, ProductMapper>();
            services.AddScoped<IProductService, ProductService>();

            return services;
        }
    }
}
