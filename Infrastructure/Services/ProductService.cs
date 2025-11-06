using List_of_products.Application.Interfaces;
using List_of_products.Application.Models;
using List_of_products.Infrastructure.ExternalModels;
using List_of_products.Infrastructure.Filters;
using List_of_products.Infrastructure.Sorters;

namespace List_of_products.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IProductMapper _mapper;

        public ProductService(IProductRepository repository, IProductMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ProductArticle>> GetProductsAsync(ArticleQuery query)
        {
            try
            {
                // Step 1: Fetch data via repository
                List<ProductDto> productDtos = await _repository.GetProductDtosAsync();

                // Step 2: Map DTOs to domain models via mapper
                IEnumerable<ProductArticle> productArticles = _mapper.MapToProductArticles(productDtos);

                // Step 3: Apply filter if provided (Open/Closed Principle)
                if (query.Filter != null)
                {
                    productArticles = query.Filter.Apply(productArticles);
                }

                // Step 4: Apply sorter if provided (Open/Closed Principle)
                if (query.Sorter != null)
                {
                    productArticles = query.Sorter.Apply(productArticles);
                }

                return productArticles.ToList();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to retrieve products.", ex);
            }
        }
    }
}
