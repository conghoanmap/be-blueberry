using blueberry.Data;
using blueberry.Interfaces;
using blueberry.Models;
using Microsoft.EntityFrameworkCore;

namespace blueberry.Services
{
    public class ColorService : IColorRepository
    {
        private readonly ApplicationDbContext _context;
        public ColorService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Color> CreateAsync(Color color)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Colors.Add(color);
                    await _context.SaveChangesAsync();
                    transaction.Commit();
                    return color;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task<List<Color>> getAllAsync()
        {
            return await _context.Colors.ToListAsync();
        }

    }
}