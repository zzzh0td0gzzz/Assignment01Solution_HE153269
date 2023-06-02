namespace BusinessObject.Client.Cart
{
    public class ProductCartModel
    {
        public int ProductId { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; } = null!;

        public string? ProductName { get; set; }

        public double Weight { get; set; }

        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }

        public double? Discount { get; set; }
    }
}
