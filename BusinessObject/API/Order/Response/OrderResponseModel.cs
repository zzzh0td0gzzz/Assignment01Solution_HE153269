using BusinessObject.API.Member.Response;

namespace BusinessObject.API.Order.Response
{
    public class OrderResponseModel
    {
        public int OrderId { get; set; }

        public int MemberId { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime? RequireDate { get; set; }

        public DateTime? ShippedDate { get; set; }

        public decimal Freight { get; set; }

        public MemberResponseModel Member { get; set; } = null!;
    }
}
