using List_of_products.Application.Interfaces;
using List_of_products.Application.Models;

namespace List_of_products.Infrastructure.Filters
{
    /// <summary>
    /// Filter products by price per litre threshold
    /// </summary>
    public class PricePerLitreFilter : IArticleFilter
    {
        private readonly decimal _maxPricePerLitre;

        public PricePerLitreFilter(decimal maxPricePerLitre)
        {
            _maxPricePerLitre = maxPricePerLitre;
        }

        public IEnumerable<ProductArticle> Apply(IEnumerable<ProductArticle> productArticles)
        {
            return productArticles.Where(a => a.PricePerUnit <= _maxPricePerLitre);
        }
    }
}
