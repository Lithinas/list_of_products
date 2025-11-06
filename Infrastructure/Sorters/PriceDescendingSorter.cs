using List_of_products.Application.Interfaces;
using List_of_products.Application.Models;

namespace List_of_products.Infrastructure.Sorters
{
    /// <summary>
    /// Sorts products by price in descending order
    /// </summary>
    public class PriceDescendingSorter : IArticleSorter
    {
        public IEnumerable<ProductArticle> Apply(IEnumerable<ProductArticle> productArticles)
        {
            return productArticles.OrderByDescending(p => p.Price);
        }
    }
}
