namespace BusinessObject.API.Order.Request
{
    public class CreateOrderRequestModel
    {
        public decimal Freight { get; set; }

        public List<CreateOrderDetailRequestModel> Details { get; set; } = new();
    }

    public class CreateOrderDetailRequestModel
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public double? Discount { get; set; }
    }
}
