using blueberry.Data;
using blueberry.Interfaces;
using blueberry.Models;
using Microsoft.EntityFrameworkCore;

namespace blueberry.Services
{
    public class UnitService : IUnitRepository
    {
        private readonly ApplicationDbContext _context;
        public UnitService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Unit> CreateAsync(Unit unit)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.Units.AddAsync(unit);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return unit;
                }
                catch (System.Exception)
                {
                    await transaction.RollbackAsync();
                    return null;
                }
            }
        }

        public async Task<List<Unit>> getAllAsync()
        {
            return await _context.Units.ToListAsync();
        }

    }
}