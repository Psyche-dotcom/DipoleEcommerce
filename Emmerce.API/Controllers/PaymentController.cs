using Ecommerce.API.Service.Implementation;
using Ecommerce.API.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    
    [Route("api/payment")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("checkout/{userid}")]
        public async Task<IActionResult> InitializeOrderPayment(string userid)
        {

            var result = await _paymentService.InitializePaymentAsync(userid);
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
        
        [HttpGet("webhook/flutterwave")]
        public async Task<IActionResult> WebHookPayment(string status, string tx_ref, string transaction_id)
        {

            var result = await _paymentService.PaymentWebhookAsync(status,tx_ref, transaction_id);
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
