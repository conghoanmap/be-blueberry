using blueberry.Dtos.Cart;
using blueberry.Dtos.Favourite;
using blueberry.Dtos.Order;

namespace blueberry.Dtos.Account
{
    // Dùng khi quản lý danh sách người dùng
    public class UserDisplay
    {
        public string Id { get; set; } // Id
        public string Username { get; set; } // Tên đăng nhập
        public string FullName { get; set; } // Tên đầy đủ
        public string Email { get; set; } // Email
        public string Address { get; set; } // Địa chỉ
        public string Phone { get; set; } // Phone
        public bool EmailConfirmed { get; set; } // Đã xác nhận email
        public decimal AccountBalance { get; set; } // Số dư tài khoản
        // public List<CartDisplay> Carts { get; set; } // Giỏ hàng
        // public List<OrderDisplay> Orders { get; set; } // Đơn hàng
        // public List<FavouriteDisplay> Favourites { get; set; } // Sản phẩm yêu thích
    }
}