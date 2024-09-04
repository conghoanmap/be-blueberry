using blueberry.Dtos.Product;

namespace blueberry.Dtos.Cart
{
    public class CartDisplay
    {
        public required string CartId { get; set; } // Mã giỏ hàng
        public required int Quantity { get; set; } // Số lượng
        public decimal TotalPrice { get; set; } // Tổng tiền
        public ProductDisplay Product { get; set; } // Sản phẩm
    }
}