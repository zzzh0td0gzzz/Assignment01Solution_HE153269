using BusinessObject.Models;

namespace DataAccess.Intentions
{
    public interface IOrderDetailRepository
    {
        public Task<List<OrderDetail>> GetOrderDetails(int orderId);

        public Task<OrderDetail> CreateOrderDetail(OrderDetail detail);
    }
}
