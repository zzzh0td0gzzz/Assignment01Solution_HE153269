namespace BusinessObject.API.Order.Request
{
    public class OrderRequestModel : PagingRequestModel
    {
        public DateTime? StartDate { get; set; } = null;

        public DateTime? EndDate { get; set; } = null;
    }
}
