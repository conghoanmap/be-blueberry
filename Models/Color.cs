using System.ComponentModel.DataAnnotations.Schema;

namespace blueberry.Models
{
    // Bảng Màu
    [Table("Colors")]
    public class Color
    {
        public string ColorId { get; set; } // Mã màu
        public string ColorName { get; set; } = "Default Color"; // Tên màu
    }
}