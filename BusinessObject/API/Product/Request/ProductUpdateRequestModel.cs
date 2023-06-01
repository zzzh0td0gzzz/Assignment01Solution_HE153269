namespace BusinessObject.API.Product.Request
{
    public class ProductUpdateRequestModel
    {
        public int CategoryId { get; set; }

        public string? ProductName { get; set; }

        public double Weight { get; set; }

        public decimal UnitPrice { get; set; }

        public int? UnitsInStock { get; set; }
    }
}
