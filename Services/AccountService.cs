using blueberry.Data;
using blueberry.Interfaces;
using blueberry.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace blueberry.Services
{
    public class AccountService : IAccountRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<AppUser> _signInManager;
        public AccountService(UserManager<AppUser> userManager, ApplicationDbContext context, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _context = context;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> AddToRoleAsync(AppUser user, string roleName)
        {
            return await _userManager.AddToRoleAsync(user, roleName);
        }

        public async Task<IdentityResult> ChangePasswordAsync(AppUser user, string currentPassword, string newPassword)
        {
            return await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        }

        public async Task<SignInResult> CheckPasswordSignInAsync(AppUser user, string password)
        {
            return await _signInManager.CheckPasswordSignInAsync(user, password, false);
        }

        public async Task<IdentityResult> ConfirmEmailAsync(AppUser user, string token)
        {
            return await _userManager.ConfirmEmailAsync(user, token);
        }

        public async Task<IdentityResult> CreateAsync(AppUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<AppUser> FindByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }


        public async Task<AppUser> FirstOrDefaultAsync(string username)
        {
            return await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == username.ToLower());
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(AppUser user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<IList<string>> GetRolesAsync(AppUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<AppUser> GetUserByUsername(string username)
        {
            AppUser user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == username);
            return user;
        }

        public async Task<IdentityResult> RemoveFromRoleAsync(AppUser user, string roleName)
        {
            return await _userManager.RemoveFromRoleAsync(user, roleName);
        }

        public async Task<AppUser> UpdateAsync(AppUser user)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _context.Users.Update(user);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return user;
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