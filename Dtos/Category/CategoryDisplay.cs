using blueberry.Dtos.Product;

namespace blueberry.Dtos.Category
{
    public class CategoryDisplay
    {
        public int CategoryId { get; set; } // Mã danh mục
        public required string CategoryName { get; set; } // Tên danh mục
        // public string CategoryImage { get; set; } = "file:///C:/Users/ADMIN/Documents/My%20Web%20Sites/…projects/blueberry-html/assets/img/category/4.svg"; // Hình ảnh
        // public List<ProductDisplay> Products { get; set; }
    }
}