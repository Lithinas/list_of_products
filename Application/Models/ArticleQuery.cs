using List_of_products.Application.Interfaces;

namespace List_of_products.Application.Models
{
    /// <summary>
    /// Query object encapsulating product query parameters
    /// </summary>
    public class ArticleQuery
    {
        public IArticleFilter? Filter { get; set; }
        public IArticleSorter? Sorter { get; set; }
    }
}
