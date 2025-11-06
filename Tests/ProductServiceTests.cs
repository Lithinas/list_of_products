using List_of_products.Application.Interfaces;
using List_of_products.Application.Models;
using List_of_products.Infrastructure.Services;
using List_of_products.Infrastructure.ExternalModels;
using List_of_products.Infrastructure.Filters;
using List_of_products.Infrastructure.Sorters;
using Moq;
using Xunit;

namespace List_of_products.Tests
{
    public class ProductServiceTests
    {
        private Mock<IProductRepository> GetMockRepository(List<ProductDto> productDtos)
        {
            var mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(repo => repo.GetProductDtosAsync())
                .ReturnsAsync(productDtos);
            return mockRepo;
        }

        private Mock<IProductMapper> GetMockMapper(List<ProductArticle> productArticles)
        {
            var mockMapper = new Mock<IProductMapper>();
            mockMapper.Setup(mapper => mapper.MapToProductArticles(It.IsAny<List<ProductDto>>()))
                .Returns(productArticles);
            return mockMapper;
        }

        [Fact]
        public async Task GetProductsAsync_ReturnsProducts()
        {
            // Arrange
            var mockProductDtos = new List<ProductDto>
            {
                new ProductDto
                {
                    Id = 1,
                    BrandName = "Brand A",
                    Name = "Beer A",
                    Articles = new List<ArticleDto>()
                },
                new ProductDto
                {
                    Id = 2,
                    BrandName = "Brand B",
                    Name = "Beer B",
                    Articles = new List<ArticleDto>()
                }
            };

            var mockProductArticles = new List<ProductArticle>
            {
                new ProductArticle
                {
                    Id = 1,
                    ProductName = "Beer A",
                    ShortDescription = "Description A",
                    Price = 1.5m,
                    Unit = "0.5L",
                    PricePerUnit = 3.00m,
                    PricePerUnitText = "€ 3.00/l",
                    Image = new Uri("https://example.com/image1.jpg")
                },
                new ProductArticle
                {
                    Id = 2,
                    ProductName = "Beer B",
                    ShortDescription = "Description B",
                    Price = 2m,
                    Unit = "0.5L",
                    PricePerUnit = 4.00m,
                    PricePerUnitText = "€ 4.00/l",
                    Image = new Uri("https://example.com/image2.jpg")
                }
            };

            var mockRepo = GetMockRepository(mockProductDtos);
            var mockMapper = GetMockMapper(mockProductArticles);
            var service = new ProductService(mockRepo.Object, mockMapper.Object);

            var query = new ArticleQuery();

            // Act
            var result = await service.GetProductsAsync(query);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("Beer A", result[0].ProductName);
            Assert.Equal("Beer B", result[1].ProductName);
        }

        [Fact]
        public async Task GetProductsAsync_FilterEnabled_ReturnsFilteredProducts()
        {
            // Arrange
            var mockProductDtos = new List<ProductDto>
            {
                new ProductDto
                {
                    Id = 1,
                    BrandName = "Brand A",
                    Name = "Cheap Beer",
                    Articles = new List<ArticleDto>()
                },
                new ProductDto
                {
                    Id = 2,
                    BrandName = "Brand B",
                    Name = "Expensive Beer",
                    Articles = new List<ArticleDto>()
                }
            };

            var mockProductArticles = new List<ProductArticle>
            {
                new ProductArticle
                {
                    Id = 1,
                    ProductName = "Cheap Beer",
                    ShortDescription = "Affordable beer",
                    Price = 1m,
                    Unit = "0.5L",
                    PricePerUnit = 1.50m,
                    PricePerUnitText = "€ 1.50/l",
                    Image = new Uri("https://example.com/cheap.jpg")
                },
                new ProductArticle
                {
                    Id = 2,
                    ProductName = "Expensive Beer",
                    ShortDescription = "Premium beer",
                    Price = 3m,
                    Unit = "0.5L",
                    PricePerUnit = 3.50m,
                    PricePerUnitText = "€ 3.50/l",
                    Image = new Uri("https://example.com/expensive.jpg")
                }
            };

            var mockRepo = GetMockRepository(mockProductDtos);
            var mockMapper = GetMockMapper(mockProductArticles);
            var service = new ProductService(mockRepo.Object, mockMapper.Object);

            var query = new ArticleQuery
            {
                Filter = new PricePerLitreFilter(2.0m)
            };

            // Act
            var result = await service.GetProductsAsync(query);

            // Assert
            Assert.Single(result);
            Assert.Equal("Cheap Beer", result[0].ProductName);
        }

        [Fact]
        public async Task GetProductsAsync_SortDescending_ReturnsProductsInDescendingOrder()
        {
            // Arrange
            var mockProductDtos = new List<ProductDto>
            {
                new ProductDto
                {
                    Id = 1,
                    BrandName = "Brand A",
                    Name = "Beer A",
                    Articles = new List<ArticleDto>()
                },
                new ProductDto
                {
                    Id = 2,
                    BrandName = "Brand B",
                    Name = "Beer B",
                    Articles = new List<ArticleDto>()
                }
            };

            var mockProductArticles = new List<ProductArticle>
            {
                new ProductArticle
                {
                    Id = 1,
                    ProductName = "Beer A",
                    ShortDescription = "Description A",
                    Price = 1m,
                    Unit = "0.5L",
                    PricePerUnit = 2.00m,
                    PricePerUnitText = "€ 2.00/l",
                    Image = new Uri("https://example.com/image1.jpg")
                },
                new ProductArticle
                {
                    Id = 2,
                    ProductName = "Beer B",
                    ShortDescription = "Description B",
                    Price = 2m,
                    Unit = "0.5L",
                    PricePerUnit = 4.00m,
                    PricePerUnitText = "€ 4.00/l",
                    Image = new Uri("https://example.com/image2.jpg")
                }
            };

            var mockRepo = GetMockRepository(mockProductDtos);
            var mockMapper = GetMockMapper(mockProductArticles);
            var service = new ProductService(mockRepo.Object, mockMapper.Object);

            var query = new ArticleQuery
            {
                Sorter = new PriceDescendingSorter()
            };

            // Act
            var result = await service.GetProductsAsync(query);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal(2m, result[0].Price);
            Assert.Equal(1m, result[1].Price);
        }

        [Fact]
        public async Task GetProductsAsync_RepositoryError_ThrowsInvalidOperationException()
        {
            // Arrange
            var mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(repo => repo.GetProductDtosAsync())
                .ThrowsAsync(new InvalidOperationException("Repository error"));

            var mockMapper = new Mock<IProductMapper>();
            var service = new ProductService(mockRepo.Object, mockMapper.Object);

            var query = new ArticleQuery();

            // Act & Assert
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await service.GetProductsAsync(query);
            });

            Assert.Equal("Failed to retrieve products.", exception.Message);
            Assert.IsType<InvalidOperationException>(exception.InnerException);
        }
    }
}
