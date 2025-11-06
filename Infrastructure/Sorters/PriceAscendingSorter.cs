using List_of_products.Application.Interfaces;
using List_of_products.Application.Models;

namespace List_of_products.Infrastructure.Sorters
{
    /// <summary>
    /// Sorts products by price in ascending order
    /// </summary>
    public class PriceAscendingSorter : IArticleSorter
    {
        public IEnumerable<ProductArticle> Apply(IEnumerable<ProductArticle> productArticles)
        {
            return productArticles.OrderBy(p => p.Price);
        }
    }
}
