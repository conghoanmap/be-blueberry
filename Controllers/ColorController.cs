using blueberry.Interfaces;
using blueberry.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace blueberry.Controllers
{
    [Route("api/color")]
    [ApiController]
    [Authorize]
    public class ColorController : ControllerBase
    {
        private readonly IColorRepository _colorRepository;
        public ColorController(IColorRepository colorRepository)
        {
            _colorRepository = colorRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetColors()
        {
            var colors = await _colorRepository.getAllAsync();
            return Ok(colors);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Color color)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _colorRepository.CreateAsync(color);
            return Ok("Tạo màu thành công!");
        }
    }
}