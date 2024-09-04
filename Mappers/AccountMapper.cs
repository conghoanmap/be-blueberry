using blueberry.Dtos.Account;
using blueberry.Models;

namespace blueberry.Mappers
{
    public static class AccountMapper
    {
        public static UserDisplay ModelToDisplay (this AppUser userModel){
            return new UserDisplay
            {
                Id = userModel.Id,
                Username = userModel.UserName,
                FullName = userModel.FullName,
                Email = userModel.Email,
                Address = userModel.Address,
                Phone = userModel.Phone,
                EmailConfirmed = userModel.EmailConfirmed,
                AccountBalance = userModel.AccountBalance
            };
        }
    }
}