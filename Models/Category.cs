using System.ComponentModel.DataAnnotations.Schema;

namespace blueberry.Models
{
    // Bảng thể loại
    [Table("Categories")]
    public class Category
    {
        public int CategoryId { get; set; } // Mã danh mục
        public required string CategoryName { get; set; } // Tên danh mục
        public string CategoryImage { get; set; } = "file:///C:/Users/ADMIN/Documents/My%20Web%20Sites/…projects/blueberry-html/assets/img/category/4.svg"; // Hình ảnh
        public List<Product> Products { get; set; } // Sản phẩm, dùng cho việc thống kê, v.v...
    }
}