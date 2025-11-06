using List_of_products.Application.Models;

namespace List_of_products.Application.Interfaces
{
    /// <summary>
    /// Sort strategy interface for product sorting logic
    /// Follows the Strategy Pattern for Open/Closed Principle compliance
    /// </summary>
    public interface IArticleSorter
    {
        IEnumerable<ProductArticle> Apply(IEnumerable<ProductArticle> productArticles);
    }
}
