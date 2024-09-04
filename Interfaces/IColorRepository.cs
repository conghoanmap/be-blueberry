using blueberry.Models;

namespace blueberry.Interfaces
{
    public interface IColorRepository
    {
        Task<List<Color>> getAllAsync();
        Task<Color> CreateAsync(Color color);
    }
}