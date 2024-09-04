using System.ComponentModel.DataAnnotations;

namespace blueberry.Dtos.Product
{
    // Dùng khi tạo mới hoặc thay đổi thông tin sản phẩm
    public class ProductRequest
    {
        [Required]
        [MinLength(8, ErrorMessage = "Mã sản phẩm phải có ít nhất 8 ký tự")]
        public required string ProductName { get; set; } // Tên sản phẩm
        [Required]
        public string ProductImage { get; set; } // Hình ảnh
        [Required]
        [Range(10000, 300000, ErrorMessage = "Giá sản phẩm phải từ 10.000 đến 300.000đ")]
        public required decimal Price { get; set; } // Giá
        [Range(0, 100, ErrorMessage = "Giảm giá phải từ 0 đến 100%")]
        public required int Discount { get; set; } // Giảm giá
        [Range(0, int.MaxValue, ErrorMessage = "Tồn kho phải từ 0 trở lên")]
        public required int Inventory { get; set; } // Tồn kho
        [Range(0, int.MaxValue, ErrorMessage = "Giá trị của đơn vị tính phải từ 0 trở lên")]
        public required int ValueUnit { get; set; } // Giá trị của đơn vị tính
        public string Description { get; set; } = string.Empty; // Mô tả
        public int UnitId { get; set; } // Mã đơn vị tính
        public string ColorId { get; set; } // Mã màu
        public int CategoryId { get; set; } // Mã danh mục
    }
}