using blueberry.Dtos.Cart;
using blueberry.Extensions;
using blueberry.Interfaces;
using blueberry.Mappers;
using blueberry.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace blueberry.Controllers
{
    [Route("api/cart")]
    [ApiController]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;
        private readonly UserManager<AppUser> _userManager;
        public CartController(ICartRepository cartRepository, UserManager<AppUser> userManager)
        {
            _cartRepository = cartRepository;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var username = User.GetUserName();
            var appUser = await _userManager.FindByNameAsync(username);
            var carts = await _cartRepository.getAllAsync(appUser.Id);
            var CartDtos = carts.Select(cart => cart.ModelToDisplay()).ToList();
            return Ok(CartDtos);
            // return Ok(carts);
        }

        [HttpPost]
        [Route("{productId}")]
        public async Task<IActionResult> CreateAsync([FromRoute] string productId, [FromBody] int quantity)
        {
            // User.GetUserName() trả về username của người dùng hiện tại trong token
            var username = User.GetUserName();
            var appUser = await _userManager.FindByNameAsync(username);

            var cartModel = CartMapper.RequestToModel(quantity);
            cartModel.AppUserId = appUser.Id;
            cartModel.ProductId = productId;

            var cart = await _cartRepository.CreateAsync(cartModel);
            if(cart == null)
            {
                return BadRequest("Không thể thêm vào giỏ hàng!");
            }
            return Ok("Đã thêm vào giỏ hàng!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] string id)
        {
            var cart = await _cartRepository.DeleteAsync(id);
            if (cart == null)
            {
                return NotFound();
            }
            return Ok("Đã xóa khỏi giỏ hàng!");
        }

        // [HttpPut("{id}")]
        // // Thay đổi số lượng sản phẩm trong giỏ hàng
        // public async Task<IActionResult> UpdateAsync([FromRoute] string id, [FromBody] int newQuantity)
        // {
        //     var updatedCart = await _cartRepository.UpdateAsync(id, newQuantity);
        //     if (updatedCart == null)
        //     {
        //         return NotFound();
        //     }
        //     return Ok("Đã cập nhật giỏ hàng!");
        // }
        [HttpPut("{id}/plus")]
        public async Task<IActionResult> PlusQuantityAsync([FromRoute] string id)
        {
            var updatedCart = await _cartRepository.PlusQuantityAsync(id);
            if (!updatedCart)
            {
                return NotFound();
            }
            return Ok("Đã cập nhật giỏ hàng!");
        }
        [HttpPut("{id}/minus")]
        public async Task<IActionResult> MinusQuantityAsync([FromRoute] string id)
        {
            var updatedCart = await _cartRepository.MinusQuantityAsync(id);
            if (!updatedCart)
            {
                return NotFound();
            }
            return Ok("Đã cập nhật giỏ hàng!");
        }
    }
}