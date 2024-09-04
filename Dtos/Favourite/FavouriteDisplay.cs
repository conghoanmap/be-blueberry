using blueberry.Dtos.Account;
using blueberry.Dtos.Product;

namespace blueberry.Dtos.Favourite
{
    public class FavouriteDisplay
    {
        public int FavouriteId { get; set; } // Mã yêu thích
        public ProductDisplay Product { get; set; } // Sản phẩm
    }
}