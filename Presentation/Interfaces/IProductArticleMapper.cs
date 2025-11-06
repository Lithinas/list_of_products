using List_of_products.Application.Models;
using List_of_products.Presentation.ViewModels;

namespace List_of_products.Presentation.Interfaces
{
    public interface IProductArticleMapper
    {
        ProductArticleViewModel ToViewModel(ProductArticle domainModel);
        List<ProductArticleViewModel> ToViewModels(List<ProductArticle> domainModels);
    }
}
