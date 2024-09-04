using blueberry.Models;

namespace blueberry.Interfaces
{
    public interface ITokenRepository
    {
        Task<string> createToken(AppUser user);
    }
}