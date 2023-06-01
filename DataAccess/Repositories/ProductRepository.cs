using BusinessObject.Models;
using DataAccess.Intentions;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly Prn231As1Context _dbcontext;

        public ProductRepository(Prn231As1Context context)
        {
            _dbcontext = context;
        }

        public async Task<List<Product>> GetProduct(string? name = null, decimal? unitFrom = null, decimal? unitTo = null, int pageIndex = 1, int pageSize = 10)
        {
            var query = from product in _dbcontext.Products select product;
            if (name != null)
                query = from data in query
                        where data.ProductName != null && data.ProductName.ToLower().Contains(name)
                        select data;
            if (unitFrom != null)
                query = from data in query
                        where data.UnitPrice >= unitFrom
                        select data;
            if (unitTo != null)
                query = from data in query
                        where data.UnitPrice <= unitTo
                        select data;
            return await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
        }
    }
}
