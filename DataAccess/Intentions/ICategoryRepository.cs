using BusinessObject.Models;

namespace DataAccess.Intentions
{
    public interface ICategoryRepository
    {
        public Task<List<Category>> GetCategories();
    }
}
