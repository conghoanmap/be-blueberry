using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace blueberry.Models
{
    // Bảng sản phẩm
    [Table("Products")]
    public class Product
    {
        public string ProductId { get; set; } // Mã sản phẩm
        public required string ProductName { get; set; } // Tên sản phẩm
        public string? ProductImage { get; set;} // Hình ảnh
        [Column(TypeName = "decimal(18,2)")]
        public required decimal Price { get; set; } // Giá
        public required int Discount { get; set; } // Giảm giá
        public required int Inventory { get; set; } // Tồn kho
        public required int ValueUnit { get; set; } // Giá trị của đơn vị tính
        public string Description { get; set; } = string.Empty; // Mô tả
        public int UnitId { get; set; } // Mã đơn vị tính
        public string ColorId { get; set; } // Mã màu
        public int CategoryId { get; set; } // Mã danh mục
        public Unit Unit { get; set; } // Đơn vị tính
        public Color Color { get; set; } // Màu
        public Category Category { get; set; } // Danh mục
        public List<Assess> Assesses { get; set; } = new List<Assess>(); // Các đánh giá
        public List<OrderDetail> OrderDetails { get; set; } // Chi tiết đơn hàng, có thể dùng cho thống kê,v.v...
        
    }
}