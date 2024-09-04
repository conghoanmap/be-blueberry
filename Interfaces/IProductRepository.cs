using blueberry.Dtos.Product;
using blueberry.Helpers;
using blueberry.Models;

namespace blueberry.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> getAllAsync(QueryObject query);
        Task<Product?> GetByIdAsync(string id);
        Task<Product> CreateAsync(Product product);
        Task<Product?> UpdateAsync(string id, Product product);
        Task<Product?> DeleteAsync(string id);
        Task<string> GenerateProductIdAsync();
        Task<int> CountProductsAsync();
    }
}