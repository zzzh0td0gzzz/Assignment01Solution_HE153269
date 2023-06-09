﻿using BusinessObject.Models;
using BusinessObject;

namespace DataAccess.Intentions
{
    public interface IOrderRepository
    {
        public Task<Order?> GetOrder(int id, int? memberId = null);

        public Task<PagingModel<Order>> GetOrders(int? memberId = null, DateTime ? startDate = null, DateTime? endDate = null, int pageIndex = 1, int pageSize = 10);

        public Task<Order> CreateOrder(Order order, List<OrderDetail> details);

        public Task UpdateOrder(Order order);

        public Task DeleteOrder(int id);
    }
}
