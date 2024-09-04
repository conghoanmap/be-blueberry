using blueberry.Models;

namespace blueberry.Interfaces
{
    public interface ICartRepository
    {
        Task<List<Cart>> getAllAsync(string userId);
        Task<Cart?> GetByIdAsync(string id);
        Task<Cart> CreateAsync(Cart cart);
        Task<Cart?> DeleteAsync(string id);
        // Task<Cart?> UpdateAsync(string id, int quantity);
        Task<bool> PlusQuantityAsync(string id);
        Task<bool> MinusQuantityAsync(string id);
    }
}