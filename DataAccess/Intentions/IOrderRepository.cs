using BusinessObject.Models;
using BusinessObject;

namespace DataAccess.Intentions
{
    public interface IOrderRepository
    {
        public Task<PagingModel<Order>> GetOrders(DateTime? startDate = null, DateTime? endDate = null, int pageIndex = 1, int pageSize = 10);

        public Task<Order> CreateOrder(Order order);

        public Task UpdateOrder(Order order);

        public Task DeleteOrder(int id);
    }
}
