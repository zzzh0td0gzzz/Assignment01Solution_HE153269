using BusinessObject;
using BusinessObject.Models;

namespace DataAccess.Intentions
{
    public interface IProductRepository
    {
        public Task<Product> GetProduct(int id);

        public Task<PagingModel<Product>> GetProducts(string? name = null, bool? unitPriceSortAsc = null, int pageIndex = 1, int pageSize = 10);

        public Task<Product> CreateProduct(Product product);

        public Task UpdateProduct(Product product);

        public Task DeleteProduct(int id);
    }
}
