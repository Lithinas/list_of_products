using List_of_products.Application.Interfaces;
using List_of_products.Infrastructure.ExternalModels;
using List_of_products.Infrastructure.Configuration;
using Microsoft.Extensions.Options;

namespace List_of_products.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ProductRepository> _logger;
        private readonly string _productDataUrl;

        public ProductRepository(IHttpClientFactory httpClientFactory, ILogger<ProductRepository> logger, IOptions<ExternalServicesSettings> settings)
        {
            _httpClient = httpClientFactory.CreateClient("ProductDataClient");
            _logger = logger;
            _productDataUrl = settings.Value.ProductDataUrl;
        }

        public async Task<List<ProductDto>> GetProductDtosAsync()
        {
            try
            {
                List<ProductDto>? products = await _httpClient.GetFromJsonAsync<List<ProductDto>>(_productDataUrl);
                if (products is null)
                {
                    _logger.LogWarning($"No products found for the {{ProductDataUrl}}", _productDataUrl);
                    return new List<ProductDto>();
                }

                return products;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "HTTP request failed while fetching products from {ProductDataUrl}", _productDataUrl);
                throw new InvalidOperationException($"Failed to fetch products from external service: {_productDataUrl}", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while fetching products from {ProductDataUrl}", _productDataUrl);
                throw;
            }
        }
    }
}
