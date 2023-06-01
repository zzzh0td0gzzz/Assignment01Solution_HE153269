namespace BusinessObject.API.Order.Response
{
    public class OrderInfoResponseModel : OrderResponseModel
    {
        public List<OrderDetailResponseModel> Details { get; set; } = new();
    }
}
