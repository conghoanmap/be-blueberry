using blueberry.Dtos.Favourite;
using blueberry.Models;

namespace blueberry.Mappers
{
    public static class FavouriteMapper
    {
        public static Favourite RequestToModel(this string productId)
        {
            return new Favourite
            {
                ProductId = productId
            };
        }
        public static FavouriteDisplay ModelToDisplay(this Favourite favourite)
        {
            return new FavouriteDisplay
            {
                FavouriteId = favourite.FavouriteId,
                Product = favourite.Product.ModelToDisplay()
            };
        }
    }
}