using blueberry.Models;

namespace blueberry.Interfaces
{
    public interface IFavouriteRepository
    {
        Task<List<Favourite>> getAllAsync(string userId);
        Task<Favourite> CreateAsync(Favourite favourite);
        Task<Favourite?> DeleteAsync(int id);
    }
}