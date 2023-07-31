using Ecommerce.API.Service.Interface;
using Ecommerce.Model.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/delivery/pickup")]
    [ApiController]
    public class DeliveryController : ControllerBase
    {
        private readonly IDeliveryService _delivery;

        public DeliveryController(IDeliveryService delivery)
        {
            _delivery = delivery;
        }
        [HttpGet("{userid}")]
        public async Task<IActionResult> GetUserPickUpLocation(string userid)
        {

            var result = await _delivery.GetDeliveryAddressAsync(userid);
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
        [HttpPost("add/{userid}")]
        public async Task<IActionResult> CreateUserPickUpLocation(string userid, AddDeliveryDto deliveryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _delivery.CreateDeliveryAddressAsync(deliveryDto, userid);
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
        [HttpPatch("update/{userid}")]
        public async Task<IActionResult> UpdateDeliveryAddressAsync(string userid, UpdateDeliveryDto deliveryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _delivery.UpdateDeliveryAddressAsync(deliveryDto, userid);
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
        [HttpGet("request/{userid}")]
        public async Task<IActionResult> RequestDeliveryfee(string userid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var result = await _delivery.GetShippingfeeQoute(userid);
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
