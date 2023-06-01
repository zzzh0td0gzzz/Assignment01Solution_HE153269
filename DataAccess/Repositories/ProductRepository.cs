using BusinessObject;
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

        public async Task<Product> GetProduct(int id)
        {
            return await _dbcontext.Products.Where(p => p.ProductId == id)
                .Include(p => p.Category).FirstAsync();
        }

        public async Task<PagingModel<Product>> GetProducts(string? name = null, bool? unitPriceSortAsc = null, int pageIndex = 1, int pageSize = 10)
        {
            var query = from product in _dbcontext.Products select product;
            if (name != null)
                query = from data in query
                        where data.ProductName != null && data.ProductName.ToLower().Contains(name.ToLower())
                        select data;
            if (unitPriceSortAsc == true)
                query = from data in query
                        orderby data.UnitPrice
                        select data;
            else if (unitPriceSortAsc == false)
                query = from data in query
                        orderby data.UnitPrice descending
                        select data;

            var items = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize)
                .Include(x => x.Category)
                .ToListAsync();
            var total = await query.CountAsync();
            return new PagingModel<Product>
            {
                Items = items,
                TotalPage = (int)Math.Ceiling((double)total / pageSize)
            };
        }

        public async Task<Product> CreateProduct(Product product)
        {
            product.Category = null!;
            product.OrderDetails = null!;
            await _dbcontext.AddAsync(product);
            await _dbcontext.SaveChangesAsync();

            product = await _dbcontext.Products.Where(pro => pro.ProductId == product.ProductId).Include(x => x.Category).FirstAsync();
            return product;
        }

        public async Task UpdateProduct(Product product)
        {
            product.Category = null!;
            product.OrderDetails = null!;
            _dbcontext.Update(product);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task DeleteProduct(int id)
        {
            await _dbcontext.Products.Where(pro => pro.ProductId == id)
                .ExecuteDeleteAsync();
        }
    }
}
