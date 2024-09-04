using blueberry.Models;

namespace blueberry.Interfaces
{
    public interface IUnitRepository
    {
        Task<List<Unit>> getAllAsync();
        Task<Unit> CreateAsync(Unit unit);
    }
}