using Ecommerce.Model.DTO;
using Emmerce.API.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [Authorize(AuthenticationSchemes = "Bearer", Roles ="Admin")]
        [HttpPost("add")]
        public async Task<IActionResult> AddProduct(AddProductDto product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.AddProductAsync(product);
            if (result.StatusCode == 200 || result.StatusCode < 300)
            {
                return Ok(result);
            }
            else if (result.StatusCode == 404)
            {
                return NotFound(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [Authorize(AuthenticationSchemes = "Bearer", Roles ="Admin")]
        [HttpPost("specification/add")]
        public async Task<IActionResult> AddProductSpecification(AddProductSpecificationDto productSpec)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.AddProductSpecificationAsync(productSpec);
            if (result.StatusCode == 200 || result.StatusCode < 300)
            {
                return Ok(result);
            }
            else if (result.StatusCode == 404)
            {
                return NotFound(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductbyId(string id)
        {
            var result = await _productService.GetProductProductbyIdAsync(id);
            if (result.StatusCode == 200 || result.StatusCode < 300)
            {
                return Ok(result);
            }
            else if (result.StatusCode == 404)
            {
                return NotFound(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [HttpGet("all/{pagesize}/{pagenumber}")]
        public async Task<IActionResult> GetAllProduct(int pagesize, int pagenumber)
        {
            var result = await _productService.GetAllProductAsync(pagesize, pagenumber);
            if (result.StatusCode == 200 || result.StatusCode < 300)
            {
                return Ok(result);
            }
            else if (result.StatusCode == 404)
            {
                return NotFound(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [HttpGet("search")]
        public async Task<IActionResult> GetAllProduct(string? searchterm, decimal? maxprice, decimal? minprice, int pagenumber)
        {
            var result = await _productService.SearchProductAsync(minprice, maxprice, searchterm, pagenumber);
            if (result.StatusCode == 200 || result.StatusCode < 300)
            {
                return Ok(result);
            }
            else if (result.StatusCode == 404)
            {
                return NotFound(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpPatch("update/{id}")]
        public async Task<IActionResult> UpdateProduct(string id, UpdateProductDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.UpdateProductAsync(id, productDto);
            if (result.StatusCode == 200 || result.StatusCode < 300)
            {
                return Ok(result);
            }
            else if (result.StatusCode == 404)
            {
                return NotFound(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var result = await _productService.DeleteProductbyIdAsync(id);
            if (result.StatusCode == 200 || result.StatusCode < 300)
            {
                return Ok(result);
            }
            else if (result.StatusCode == 404)
            {
                return NotFound(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}
