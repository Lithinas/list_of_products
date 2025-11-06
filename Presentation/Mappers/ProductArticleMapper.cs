using List_of_products.Application.Models;
using List_of_products.Presentation.Interfaces;
using List_of_products.Presentation.ViewModels;

namespace List_of_products.Presentation.Mappers
{
    public class ProductArticleMapper : IProductArticleMapper
    {
        public ProductArticleViewModel ToViewModel(ProductArticle domainModel)
        {
            return new ProductArticleViewModel
            {
                Id = domainModel.Id,
                ProductName = domainModel.ProductName,
                ShortDescription = domainModel.ShortDescription,
                Price = domainModel.Price,
                Unit = domainModel.Unit,
                PricePerUnitText = domainModel.PricePerUnitText,
                Image = domainModel.Image
            };
        }

        public List<ProductArticleViewModel> ToViewModels(List<ProductArticle> domainModels)
        {
            return domainModels.Select(ToViewModel).ToList();
        }
    }
}
