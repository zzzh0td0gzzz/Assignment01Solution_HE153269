using BusinessObject.Models;

namespace DataAccess.Intentions
{
    public interface ICategoryRepository
    {
        public Task<List<Category>> GetCategories();

        public Task<Category> CreateCategory(Category category);

        public Task UpdateCategory(Category category);

        public Task DeleteCategory(int id);
    }
}
