using BusinessObject.Models;
using DataAccess.Intentions;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly Prn231As1Context _dbcontext;

        public CategoryRepository(Prn231As1Context context)
        {
            _dbcontext = context;
        }

        public async Task<List<Category>> GetCategories()
        {
            return await _dbcontext.Categories.ToListAsync();
        }

        public async Task<Category> CreateCategory(Category category)
        {
            category.Products = null!;
            await _dbcontext.AddAsync(category);
            await _dbcontext.SaveChangesAsync();
            return category;
        }

        public async Task UpdateCategory(Category category)
        {
            category.Products = null!;
            _dbcontext.Update(category);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task DeleteCategory(int id)
        {
            await _dbcontext.Categories.Where(cate => cate.CategoryId == id).ExecuteDeleteAsync();
        }
    }
}
