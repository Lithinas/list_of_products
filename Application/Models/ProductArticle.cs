namespace List_of_products.Application.Models
{
    public class ProductArticle
    {
        public required int Id { get; set; }
        public required string ProductName { get; set; }
        public required string ShortDescription { get; set; }
        public required decimal Price { get; set; }
        public required string Unit { get; set; }
        public required decimal PricePerUnit { get; set; }
        public required string PricePerUnitText { get; set; }
        public required Uri Image { get; set; }
    }
}
