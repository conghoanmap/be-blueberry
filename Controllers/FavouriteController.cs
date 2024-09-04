using blueberry.Dtos.Favourite;
using blueberry.Extensions;
using blueberry.Interfaces;
using blueberry.Mappers;
using blueberry.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace blueberry.Controllers
{
    [ApiController]
    [Route("api/favourite")]
    [Authorize]
    public class FavouriteController : ControllerBase
    {
        private readonly IFavouriteRepository _favouriteRepository;
        private readonly UserManager<AppUser> _userManager;
        public FavouriteController(IFavouriteRepository favouriteRepository, UserManager<AppUser> userManager)
        {
            _favouriteRepository = favouriteRepository;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllAsync()
        {
            var username = User.GetUserName();
            var appUser = await _userManager.FindByNameAsync(username);
            var favourites = await _favouriteRepository.getAllAsync(appUser.Id);
            return Ok(favourites.Select(favourite => favourite.ModelToDisplay()));
        }

        [HttpPost("{productId}")]
        [Authorize]
        public async Task<IActionResult> CreateAsync([FromRoute] string productId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var favourite = FavouriteMapper.RequestToModel(productId);

            var username = User.GetUserName();
            var appUser = await _userManager.FindByNameAsync(username);
            favourite.AppUserId = appUser.Id;

            await _favouriteRepository.CreateAsync(favourite);
            return Ok("Đã thêm vào yêu thích!");
        }

        [HttpDelete("{favouriteId}")]
        [Authorize]
        public async Task<IActionResult> DeleteAsync([FromRoute] int favouriteId)
        {
            var result = await _favouriteRepository.DeleteAsync(favouriteId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok("Đã xóa khỏi yêu thích!");
        }
    }
}