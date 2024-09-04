using blueberry.Dtos.Product;

namespace blueberry.Dtos.Order
{
    public class OrderDetailDisplay
    {
        public int Quantity { get; set; } // Số lượng
        public decimal TotalPrice { get; set; } // Giá
        public ProductDisplay Product { get; set; } // Sản phẩm
    }
}