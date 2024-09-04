using blueberry.Dtos.Cart;
using blueberry.Models;

namespace blueberry.Mappers
{
    public static class CartMapper
    {
        public static CartDisplay ModelToDisplay(this Cart cart)
        {
            return new CartDisplay
            {
                CartId = cart.CartId,
                Quantity = cart.Quantity,
                TotalPrice = cart.TotalPrice,
                Product = cart.Product.ModelToDisplay()
            };
        }
        public static Cart RequestToModel(this int quantity)
        {
            return new Cart
            {
                Quantity = quantity
            };
        }
    }
}