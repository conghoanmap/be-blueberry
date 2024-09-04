using blueberry.Models;

namespace blueberry.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> getAllAsync();
        Task<Category> CreateAsync(Category category);
        Task<Category?> DeleteAsync(int id);
    }
}