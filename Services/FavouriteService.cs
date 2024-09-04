using blueberry.Data;
using blueberry.Interfaces;
using blueberry.Models;
using Microsoft.EntityFrameworkCore;

namespace blueberry.Services
{
    public class FavouriteService : IFavouriteRepository
    {
        private readonly ApplicationDbContext _context;
        public FavouriteService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Favourite> CreateAsync(Favourite favourite)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var existingFavourite = await _context.Favourites.FirstOrDefaultAsync(f => f.ProductId == favourite.ProductId && f.AppUserId == favourite.AppUserId);
                    if (existingFavourite != null)
                    {
                        return existingFavourite;
                    }
                    await _context.Favourites.AddAsync(favourite);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return favourite;
                }
                catch (System.Exception)
                {
                    await transaction.RollbackAsync();
                    return null;
                }
            }
        }

        public async Task<Favourite?> DeleteAsync(int id)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var favourite = await _context.Favourites.FindAsync(id);
                    if (favourite == null)
                    {
                        return null;
                    }
                    _context.Favourites.Remove(favourite);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return favourite;
                }
                catch (System.Exception)
                {
                    await transaction.RollbackAsync();
                    return null;
                }
            }
        }

        public async Task<List<Favourite>> getAllAsync(string userId)
        {
            return await _context.Favourites
            .Include(p => p.Product)
            .ThenInclude(c => c.Category)
            .Include(p => p.Product)
            .ThenInclude(u => u.Unit)
            .Where(f => f.AppUserId == userId).ToListAsync();
        }
    }
}