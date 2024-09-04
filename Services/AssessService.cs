using blueberry.Data;
using blueberry.Interfaces;
using blueberry.Models;

namespace blueberry.Services
{
    public class AssessService : IAssessRepository
    {
        private readonly ApplicationDbContext _context;
        public AssessService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Assess> CreateAsync(Assess assess)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.Assesses.AddAsync(assess);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return assess;
                }
                catch (System.Exception)
                {
                    await transaction.RollbackAsync();
                    return null;
                }
            }
        }

    }
}