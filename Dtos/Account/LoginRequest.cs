using System.ComponentModel.DataAnnotations;

namespace blueberry.Dtos.Account
{
    // Dùng khi người dùng đăng nhập
    public class LoginRequest
    {
        [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập")]
        public string Username { get; set; } // Email
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        public string Password { get; set; } // Mật khẩu
    }
}