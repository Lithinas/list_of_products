using List_of_products.Application.Interfaces;
using List_of_products.Application.Models;

namespace List_of_products.Infrastructure.Filters
{
    /// <summary>
    /// No-op filter that returns all products (Null Object Pattern)
    /// </summary>
    public class NoFilter : IArticleFilter
    {
        public IEnumerable<ProductArticle> Apply(IEnumerable<ProductArticle> productArticles)
        {
            return productArticles;
        }
    }
}
