using List_of_products.Application.Interfaces;
using List_of_products.Application.Models;
using List_of_products.Infrastructure.Filters;
using List_of_products.Infrastructure.Sorters;
using List_of_products.Presentation.Interfaces;
using List_of_products.Presentation.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace List_of_products.Presentation.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(
        IProductService productService,
        IProductArticleMapper productArticleMapper
    ) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(List<ProductArticleViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProductsAsync(
            [FromQuery] bool filterForTwoEurosPerLitreOrUnder = false,
            [FromQuery] string? sortOrder = "asc")
        {
            try
            {
                var query = BuildQuery(filterForTwoEurosPerLitreOrUnder, sortOrder);

                List<ProductArticle> productArticles = await productService.GetProductsAsync(query);
                if (productArticles is null)
                {
                    return BadRequest();
                }

                List<ProductArticleViewModel> viewModelProductArticles = productArticleMapper.ToViewModels(productArticles);

                return Ok(viewModelProductArticles);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "An error occurred while processing your request.", details = ex.Message });
            }
        }

        /// <summary>
        /// Translates HTTP query parameters into a ProductQuery object
        /// </summary>
        private ArticleQuery BuildQuery(bool filterForTwoEurosPerLitreOrUnder, string? sortOrder)
        {
            var query = new ArticleQuery();

            if (filterForTwoEurosPerLitreOrUnder)
            {
                query.Filter = new PricePerLitreFilter(2.0m);
            }

            // Set sorter based on HTTP parameter
            query.Sorter = sortOrder?.ToLower() switch
            {
                "asc" => new PriceAscendingSorter(),
                "desc" => new PriceDescendingSorter(),
                _ => null // No sorting if invalid or null
            };

            return query;
        }
    }
}
