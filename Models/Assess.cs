using System.ComponentModel.DataAnnotations.Schema;

namespace blueberry.Models
{
    // Bảng Đánh giá/Bình luận
    [Table("Assesses")]
    public class Assess
    {
        public int AssessId { get; set; } // Mã đánh giá
        public string ProductId { get; set; } // Mã sản phẩm
        public DateTime AssessDate { get; set; } = DateTime.Now; // Ngày đánh giá
        public required int StarValue { get; set; } // Số sao
        public required string Comment { get; set; } // Nội dung
        public string AppUserId { get; set; } // Mã người dùng
        public AppUser AppUser { get; set; } // Người dùng
        public Product Product { get; set; } // Sản phẩm
    }
}