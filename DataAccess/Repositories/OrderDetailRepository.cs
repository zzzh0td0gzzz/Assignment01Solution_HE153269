using BusinessObject;
using BusinessObject.Models;
using DataAccess.Intentions;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly Prn231As1Context _dbcontext;

        public OrderDetailRepository(Prn231As1Context context)
        {
            _dbcontext = context;
        }

        public async Task<PagingModel<OrderDetail>> GetOrderDetails(int orderId, int pageIndex = 1, int pageSize = 10)
        {
            var query = from orderDetail in _dbcontext.OrderDetails
                        where orderDetail.OrderId == orderId
                        select orderDetail;
            var items = await query.Skip(pageSize * (pageIndex - 1))
                .Take(pageSize)
                .Include(o => o.Product)
                .ToListAsync();
            var total = await query.CountAsync();
            return new PagingModel<OrderDetail>
            {
                Items = items,
                TotalPage = (int)Math.Ceiling((double)total / pageSize)
            };
        }

        public async Task<OrderDetail> CreateOrderDetail(OrderDetail detail)
        {
            detail.Order = null!;
            detail.Product = null!;
            await _dbcontext.OrderDetails.AddAsync(detail);
            await _dbcontext.SaveChangesAsync();
            return detail;
        }
    }
}
