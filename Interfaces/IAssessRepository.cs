using blueberry.Models;

namespace blueberry.Interfaces
{
    public interface IAssessRepository
    {
        Task<Assess> CreateAsync(Assess category);
    }
}