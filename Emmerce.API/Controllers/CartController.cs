using Ecommerce.API.Service.Interface;
using Ecommerce.Model.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        [HttpGet("{userid}")]
        public async Task<IActionResult> GetUserCart(string userid)
        {

            var result = await _cartService.GetCartbyUserIdAsync(userid);
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
        [HttpPost("add")]
        public async Task<IActionResult> CreateUserCart(string userid)
        {

            var result = await _cartService.CreateCartAsync(userid);
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
        [HttpPost("cartitem/add")]
        public async Task<IActionResult> AddProductToUserCart(AddCartItemDto product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _cartService.AddCartItemAsync(product);
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
        [HttpDelete("delete/{userid}/{productid}")]
        public async Task<IActionResult> DeleteUserCartItem(string productid, string userid)
        {

            var result = await _cartService.RemoveUserCartItemAsync(userid, productid);
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
        [HttpPatch("update")]
        public async Task<IActionResult> UpdateUserCartItem(UpdateUserCartItemDto cartItemDto)
        {

            var result = await _cartService.UpdateUserCartItemAsync(cartItemDto.UserId, cartItemDto.ProductId, cartItemDto.Quantity);
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
