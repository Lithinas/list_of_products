using List_of_products.Application.Models;

namespace List_of_products.Application.Interfaces
{
    /// <summary>
    /// Filter specification interface for product filtering logic
    /// Follows the Specification Pattern for Open/Closed Principle compliance
    /// </summary>
    public interface IArticleFilter
    {
        IEnumerable<ProductArticle> Apply(IEnumerable<ProductArticle> productArticles);
    }
}
