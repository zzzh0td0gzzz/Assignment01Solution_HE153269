namespace BusinessObject.API.Product.Response
{
    public class ProductResponseModel
    {
        public int ProductId { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; } = null!;

        public string? ProductName { get; set; }

        public double Weight { get; set; }

        public decimal UnitPrice { get; set; }

        public int? UnitsInStock { get; set; }
    }
}
