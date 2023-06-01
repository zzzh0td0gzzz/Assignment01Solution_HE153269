using BusinessObject;
using BusinessObject.Models;
using DataAccess.Intentions;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly Prn231As1Context _dbcontext;

        public OrderRepository(Prn231As1Context context)
        {
            _dbcontext = context;
        }

        public async Task<Order?> GetOrder(int id, int? memberId = null)
        {
            return await _dbcontext.Orders.Where(o => o.OrderId == id && (memberId == null || o.MemberId == memberId))
                .Include(o => o.Member).FirstOrDefaultAsync();
        }

        public async Task<PagingModel<Order>> GetOrders(int? memberId = null, DateTime? startDate = null, DateTime? endDate = null, int pageIndex = 1, int pageSize = 10)
        {
            var query = from order in _dbcontext.Orders select order;
            if (memberId != null)
                query = from order in query
                        where order.MemberId == memberId
                        select order;
            if (startDate != null)
                query = from order in query
                        where order.OrderDate >= startDate
                        select order;
            if (endDate != null)
                query = from order in query
                        where order.OrderDate <= endDate
                        select order;
            var items = await query.OrderBy(x => x.OrderDate)
                .Skip(pageSize * (pageIndex - 1))
                .Take(pageSize)
                .Include(o => o.Member)
                .ToListAsync();
            var total = await query.CountAsync();
            return new PagingModel<Order>
            {
                Items = items,
                TotalPage = (int)Math.Ceiling((double)total / pageSize)
            };
        }

        public async Task<Order> CreateOrder(Order order)
        {
            order.Member = null!;
            order.OrderDetails = null!;
            await _dbcontext.AddAsync(order);
            await _dbcontext.SaveChangesAsync();
            return await _dbcontext.Orders.Where(o => o.OrderId == order.OrderId)
                .Include(o => o.Member)
                .FirstAsync();
        }

        public async Task UpdateOrder(Order order)
        {
            order.Member = null!;
            order.OrderDetails = null!;
            _dbcontext.Update(order);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task DeleteOrder(int id)
        {
            await _dbcontext.Orders.Where(o => o.OrderId == id).ExecuteDeleteAsync();
        }
    }
}
