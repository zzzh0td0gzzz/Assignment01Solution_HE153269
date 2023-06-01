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
    }
}
