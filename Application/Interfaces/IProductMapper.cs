using List_of_products.Application.Models;
using List_of_products.Infrastructure.ExternalModels;

namespace List_of_products.Application.Interfaces
{
    /// <summary>
    /// Mapper interface for converting DTOs to models
    /// </summary>
    public interface IProductMapper
    {
        List<ProductArticle> MapToProductArticles(List<ProductDto> productDtos);
    }
}
