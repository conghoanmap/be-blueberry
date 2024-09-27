using blueberry.Dtos.Product;
using blueberry.Helpers;
using blueberry.Interfaces;
using blueberry.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using blueberry.Filters;

namespace blueberry.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        [Route("count")]
        public async Task<IActionResult> CountProducts()
        {
            var count = await _productRepository.CountProductsAsync();
            return Ok(count);
        }
        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] QueryObject query)
        {
            var products = await _productRepository.getAllAsync(query);
            // Chuyển thành ProductDto nếu cần
            // ...
            var productDto = products.Select(product => product.ModelToDisplay()).ToList();
            return Ok(productDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById([FromRoute] string id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product.ModelToDisplay());
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] ProductRequest productRequest) // Giống như @RequestBody trong Spring
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var productModel = productRequest.RequestToModel();
            var productCreate = await _productRepository.CreateAsync(productModel);
            return Ok(productCreate.ModelToDisplay());
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] ProductRequest productRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var productModel = productRequest.RequestToModel();

            var result = await _productRepository.UpdateAsync(id, productModel);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result.ModelToDisplay());
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var productModel = await _productRepository.DeleteAsync(id);

            if (productModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}