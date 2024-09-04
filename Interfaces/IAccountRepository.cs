using blueberry.Models;
using Microsoft.AspNetCore.Identity;

namespace blueberry.Interfaces
{
    public interface IAccountRepository
    {
        Task<IList<string>> GetRolesAsync(AppUser user);
        // Tạo token xác nhận mail
        Task<string> GenerateEmailConfirmationTokenAsync(AppUser user);
        // Xác nhận mail
        Task<IdentityResult> ConfirmEmailAsync(AppUser user, string token);
        // Gán quyền cho tài khoản
        Task<IdentityResult> AddToRoleAsync(AppUser user, string roleName);
        // Hủy bỏ quyền cho tài khoản
        Task<IdentityResult> RemoveFromRoleAsync(AppUser user, string roleName);
        // Tạo tài khoản
        Task<IdentityResult> CreateAsync(AppUser user, string password);
        // Đổi mật khẩu
        Task<IdentityResult> ChangePasswordAsync(AppUser user, string currentPassword, string newPassword);
        // Kiểm tra mật khẩu
        Task<SignInResult> CheckPasswordSignInAsync(AppUser user, string password);
        // Lấy tài khoản theo mail
        Task<AppUser> FindByEmailAsync(string email);
        // Lấy tài khoản đầu tiên theo username
        Task<AppUser> FirstOrDefaultAsync(string username);
        // Cập nhật tài khoản
        Task<AppUser> UpdateAsync(AppUser user);
    }
}