namespace List_of_products.Infrastructure.ExternalModels
{
    /// <summary>
    /// DTO for deserializing Product data from external API
    /// </summary>
    public class ProductDto
    {
        public required int Id { get; set; }
        public required string BrandName { get; set; }
        public required string Name { get; set; }
        public string? DescriptionText { get; set; } = "";
        public required List<ArticleDto> Articles { get; set; }
    }
}
