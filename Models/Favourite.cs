using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace blueberry.Models
{
    // Bảng yêu thích của người dùng
    [Table("Favourites")]
    public class Favourite
    {
        public int FavouriteId { get; set; } // Mã yêu thích
        public string AppUserId { get; set; } // Mã người dùng
        public required string ProductId { get; set; } // Mã sản phẩm
        public AppUser AppUser { get; set; } // Người dùng
        public Product Product { get; set; } // Sản phẩm
    }
}