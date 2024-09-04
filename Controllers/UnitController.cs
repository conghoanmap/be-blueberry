using blueberry.Interfaces;
using blueberry.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace blueberry.Controllers
{
    [Route("api/unit")]
    [ApiController]
    [Authorize]
    public class UnitController : ControllerBase
    {   
        private readonly IUnitRepository _unitRepository;
        public UnitController(IUnitRepository unitRepository)
        {
            _unitRepository = unitRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetUnits()
        {
            var colors = await _unitRepository.getAllAsync();
            return Ok(colors);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Unit unit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _unitRepository.CreateAsync(unit);
            return Ok("Tạo màu thành công!");
        }
    }
}