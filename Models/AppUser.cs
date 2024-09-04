using Microsoft.AspNetCore.Identity;

namespace blueberry.Models
{
    // Bảng User(Người dùng)
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; } // Tên đầy đủ
        public decimal AccountBalance { get; set; } = 1000000; // Số dư tài khoản
        public string Address { get; set; } = string.Empty; // Địa chỉ
        public string Phone { get; set; } = string.Empty; // Phone
        public List<Cart> Carts { get; set; } // Giỏ hàng
        public List<Order> Orders { get; set; } // Đơn hàng
        public List<Favourite> Favourites { get; set; } // Sản phẩm yêu thích
    }
}