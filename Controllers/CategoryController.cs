using blueberry.Interfaces;
using blueberry.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace blueberry.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryRepository.getAllAsync();
            return Ok(categories);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _categoryRepository.CreateAsync(category);
            return Ok("Tạo danh mục thành công!");
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryRepository.DeleteAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok("Xóa danh mục thành công!");
        }
        
    }
}