using blueberry.Dtos.Assess;
using blueberry.Extensions;
using blueberry.Interfaces;
using blueberry.Mappers;
using blueberry.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace blueberry.Controllers
{
    [Route("api/assess")]
    [ApiController]
    [Authorize]
    public class AssessController : ControllerBase
    {
        private readonly IAssessRepository _assessRepository;
        private readonly UserManager<AppUser> _userManager;
        public AssessController(IAssessRepository assessRepository, UserManager<AppUser> userManager)
        {
            _assessRepository = assessRepository;
            _userManager = userManager;
        }

        // Đánh giá
        [HttpPost]
        [Route("{productId}")]
        public async Task<IActionResult> Create([FromRoute] string productId, [FromBody] AssessRequest assessRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var username = User.GetUserName();
            var appUser = await _userManager.FindByNameAsync(username);

            var assessModel = assessRequest.RequestToModel();
            assessModel.ProductId = productId;
            assessModel.AppUserId = appUser.Id;

            await _assessRepository.CreateAsync(assessModel);
            return Ok("Cảm ơn đánh giá của bạn!");
        }
    }
}