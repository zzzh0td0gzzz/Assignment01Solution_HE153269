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

        public async Task<List<OrderDetail>> GetOrderDetails(int orderId)
        {
            var query = from orderDetail in _dbcontext.OrderDetails
                        where orderDetail.OrderId == orderId
                        select orderDetail;
            return await query.AsNoTracking().Include(o => o.Product).ToListAsync();
        }

        public async Task<OrderDetail> CreateOrderDetail(OrderDetail detail)
        {
            detail.Order = null!;
            detail.Product = null!;
            await _dbcontext.OrderDetails.AddAsync(detail);
            await _dbcontext.SaveChangesAsync();
            return await _dbcontext.OrderDetails.AsNoTracking()
                .Where(d => d.OrderId == detail.OrderId && d.ProductId == detail.ProductId)
                .Include(o => o.Product)
                .FirstAsync();
        }
    }
}
