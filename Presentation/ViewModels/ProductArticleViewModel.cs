namespace List_of_products.Presentation.ViewModels
{
    public class ProductArticleViewModel
    {
        public required int Id { get; set; }
        public required string ProductName { get; set; }
        public required string ShortDescription { get; set; }
        public required decimal Price { get; set; }
        public required string Unit { get; set; }
        public required string PricePerUnitText { get; set; }
        public required Uri Image { get; set; }
    }
}
