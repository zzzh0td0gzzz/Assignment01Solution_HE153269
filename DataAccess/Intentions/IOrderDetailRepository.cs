using BusinessObject.Models;
using BusinessObject;

namespace DataAccess.Intentions
{
    public interface IOrderDetailRepository
    {
        public Task<PagingModel<OrderDetail>> GetOrderDetails(int orderId, int pageIndex = 1, int pageSize = 10);

        public Task<OrderDetail> CreateOrderDetail(OrderDetail detail);
    }
}
