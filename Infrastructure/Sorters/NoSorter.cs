using List_of_products.Application.Interfaces;
using List_of_products.Application.Models;

namespace List_of_products.Infrastructure.Sorters
{
    /// <summary>
    /// No-op sorter that maintains original order (Null Object Pattern)
    /// </summary>
    public class NoSorter : IArticleSorter
    {
        public IEnumerable<ProductArticle> Apply(IEnumerable<ProductArticle> productArticles)
        {
            return productArticles;
        }
    }
}
