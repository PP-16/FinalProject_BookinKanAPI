using BookinKanAPI.DTOs;
using BookinKanAPI.Models;
using BookinKanAPI.ServicesManage.PaymentServiceManage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookinKanAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> getPayment()
        {
            return Ok(await _paymentService.GetPayment());
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> CreatePayment([FromForm] PaymentDTO payment)
        {
            var result = await _paymentService.CreateOrUpdatePayment(payment);
            if (result == null) return BadRequest();

            return StatusCode(StatusCodes.Status200OK, result);
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> UpdateStripePayment(int Id, int PaymentId)
        {
            var result = await _paymentService.UpdatePaymentStripe(Id, PaymentId);
            if (result == null) return BadRequest();

            return StatusCode(StatusCodes.Status200OK, result);
        }



        [HttpPost("[action]")]
        public async Task<IActionResult> RefundPayment([FromBody] string paymentIntentId)
        {
            try
            {
                var refund = await _paymentService.RefundPayment(paymentIntentId);
                // Handle success
                return Ok(new { Message = "Refund successful" });
            }
            catch (Exception ex)
            {
                // Handle error
                return BadRequest(new { Message = ex.Message });
            }
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> UpdateStatusPayments(int ID, Status newStatus)
        {
            var result = await _paymentService.UpdateStatusPayment(ID, newStatus);
            if (result != null) return BadRequest(result);
            return Ok(StatusCodes.Status200OK);
        }
    }
}
