using blueberry.Dtos.Assess;
using blueberry.Dtos.Category;
using blueberry.Dtos.Order;
using blueberry.Models;

namespace blueberry.Dtos.Product
{
    // Dùng khi hiển thị thông tin sản phẩm
    public class ProductDisplay
    {
        public string ProductId { get; set; } // Mã sản phẩm
        public required string ProductName { get; set; } // Tên sản phẩm
        public string? ProductImage { get; set;} // Hình ảnh
        public required decimal Price { get; set; } // Giá
        public required int Discount { get; set; } // Giảm giá
        public required int Inventory { get; set; } // Tồn kho
        public required int ValueUnit { get; set; } // Giá trị của đơn vị tính
        public string Description { get; set; } = string.Empty; // Mô tả
        public Unit Unit { get; set; } // Đơn vị tính
        public Color Color { get; set; } // Màu
        public CategoryDisplay Category { get; set; } // Danh mục
        public List<AssessDisplay> Assesses { get; set; } = new List<AssessDisplay>(); // Các đánh giá
        public List<OrderDetailDisplay> OrderDetails { get; set; } = new List<OrderDetailDisplay>(); // Chi tiết đơn hàng, có thể dùng cho thống kê,v.v...
    }
}