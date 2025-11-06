using List_of_products.Infrastructure.ExternalModels;

namespace List_of_products.Application.Interfaces
{
    /// <summary>
    /// Repository interface for fetching product data from external sources
    /// </summary>
    public interface IProductRepository
    {
        Task<List<ProductDto>> GetProductDtosAsync();
    }
}
