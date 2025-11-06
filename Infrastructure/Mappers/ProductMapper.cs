using List_of_products.Application.Interfaces;
using List_of_products.Application.Models;
using List_of_products.Infrastructure.ExternalModels;
using System.Globalization;
using System.Text.RegularExpressions;

namespace List_of_products.Infrastructure.Mappers
{
    public class ProductMapper : IProductMapper
    {
        public List<ProductArticle> MapToProductArticles(List<ProductDto> productDtos)
        {
            return productDtos
                .SelectMany(p => p.Articles.Select(a => new ProductArticle
                {
                    Id = a.Id,
                    ProductName = p.Name,
                    ShortDescription = a.ShortDescription,
                    Price = a.Price,
                    Unit = a.Unit,
                    PricePerUnit = ParsePricePerUnit(a.PricePerUnitText),
                    PricePerUnitText = a.PricePerUnitText,
                    Image = a.Image
                }))
                .ToList();
        }

        /// <summary>
        /// Parses the numeric price value from formatted text (e.g., "€ 1.50/l" -> 1.50)
        /// </summary>
        private static decimal ParsePricePerUnit(string pricePerUnitText)
        {
            var match = Regex.Match(pricePerUnitText, @"[\d,\.]+");
            if (match.Success && decimal.TryParse(
                match.Value.Replace(',', '.'),
                NumberStyles.Any,
                CultureInfo.InvariantCulture,
                out decimal pricePerUnit))
            {
                return pricePerUnit;
            }
            return 0m;
        }
    }
}
