namespace MyShop.Models
{
    public class ProductViewModel
    {
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public decimal? Price { get; set; }
    }
}
