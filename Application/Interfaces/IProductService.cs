using List_of_products.Application.Models;

namespace List_of_products.Application.Interfaces
{
    public interface IProductService
    {
        /// <summary>
        /// Gets products based on the provided query specification
        /// </summary>
        /// <param name="query">Query object containing filter and sort strategies</param>
        /// <returns>List of products matching the query</returns>
        Task<List<ProductArticle>> GetProductsAsync(ArticleQuery query);
    }
}
