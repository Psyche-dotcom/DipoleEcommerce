using Ecommerce.API.Service.Interface;
using Ecommerce.Model.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("{userid}")]
        public async Task<IActionResult> GetUserActiveOrder(string userid)
        {

            var result = await _orderService.GetActiveUserOrderbyUserIdAsync(userid);
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
        public async Task<IActionResult> CreateOrder(AddOrderDto order)
        {
            var result = await _orderService.CreateOrderAsync(order);
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
        [HttpDelete("delete/{userid}")]
        public async Task<IActionResult> DeleteActiveOrder(string userid)
        {

            var result = await _orderService.RemoveActiveUserOrderbyUserIdAsync(userid);
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
