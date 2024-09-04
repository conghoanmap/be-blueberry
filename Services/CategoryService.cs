using blueberry.Data;
using blueberry.Interfaces;
using blueberry.Models;
using Microsoft.EntityFrameworkCore;

namespace blueberry.Services
{
    public class CategoryService : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Category> CreateAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<Category?> DeleteAsync(int id)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var category = await _context.Categories.FindAsync(id);
                    if (category == null)
                    {
                        return null;
                    }
                    _context.Categories.Remove(category);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return category;
                }
                catch (System.Exception)
                {
                    await transaction.RollbackAsync();
                    return null;
                }
            }
        }

        public async Task<List<Category>> getAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

    }
}