namespace List_of_products.Infrastructure.ExternalModels
{
    /// <summary>
    /// DTO for deserializing Article data from external API
    /// </summary>
    public class ArticleDto
    {
        public required int Id { get; set; }
        public required string ShortDescription { get; set; }
        public required decimal Price { get; set; }
        public required string Unit { get; set; }
        public required string PricePerUnitText { get; set; }
        public required Uri Image { get; set; }
    }
}
