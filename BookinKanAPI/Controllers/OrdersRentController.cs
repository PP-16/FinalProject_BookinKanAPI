using BookinKanAPI.DTOs.AuthenDto;
using BookinKanAPI.DTOs.RentCarsDTO;
using BookinKanAPI.Models;
using BookinKanAPI.ServicesManage.OrderServiceManage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookinKanAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersRentController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersRentController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetOrder()
        {
            List<OrderRent> orderRents = await _orderService.GetOrders();

            foreach (var orderRent in orderRents)
            {
                // Check if ConfirmReturn is false and DateTimeReturn is in the past
                if (orderRent.ConfirmReturn != true&& orderRent.OrderSatus == Status.CompletePayment && orderRent.OrderRentItems.Any(item => item.DateTimeReturn < DateTime.Now))
                {
                    // Call the method to find past due orders
                    await _orderService.CheckPastDueOrders(orderRents);
                }
            }

                    
            return Ok(orderRents);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetById(int ID)
        {
            return Ok(await _orderService.GetOrderById(ID));
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> GetOrderItem()
        {
            return Ok(await _orderService.GetOrdersItems());
        }

        [HttpGet("[action]")]
        public IActionResult GetOrderTotalByReturn()
        {
            return Ok(_orderService.GetTotalPriceByReturnDate());
        }
        [HttpGet("[action]")]
        public IActionResult GetOrderTotalByMount(int month, int year)
        {
            return Ok(_orderService.GetTotalPriceByReturnDateMount(month, year));
        }
        [HttpGet("[action]")]
        public IActionResult GetOrderTotalByYear(int year)
        {
            return Ok(_orderService.GetTotalPriceByReturnDateYear(year));
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> CreateOrders(OrderRentItemDTO request)
        {
            var result = await _orderService.CreateOrderRent(request);
            if (result == null) return BadRequest();

            return Ok(result);

        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetcarInRented()
        {
            return Ok(await _orderService.GetRentedCarsNow());
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> UpdateStatus(int ID, Status newStatus)
        {
            var result = await _orderService.UpdateStatusOrders(ID, newStatus);
            if (result != null) return BadRequest(result);
            return Ok(StatusCodes.Status200OK);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ConfirmReturn(int Id, bool confirm)
        {
            var result = await _orderService.ConfirmReturnStatus(Id, confirm);
            if (result != null) return BadRequest();

            return Ok(StatusCodes.Status200OK);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> CheckPastDueOrders()
        {
            List<OrderRent> orderRents = await _orderService.GetOrders();
            List<OrdersPastDue> pastDueOrders = new List<OrdersPastDue>(); // Initialize pastDueOrders here

            foreach (var orderRent in orderRents)
            {
                // Check if ConfirmReturn is false and DateTimeReturn is in the past
                if (orderRent.ConfirmReturn != true && orderRent.OrderRentItems.Any(item => item.DateTimeReturn < DateTime.Now))
                {
                    // Call the method to find past due orders
                    var ordersPastDue = await _orderService.CheckPastDueOrders(orderRents);
                    if (ordersPastDue != null)
                    {
                        pastDueOrders.AddRange(ordersPastDue);
                    }
                }
            }

            return Ok(pastDueOrders);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetOrderPast()
        {
            return Ok(await _orderService.GetOrdersPastDues());
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetOrderPastById(int orderRentId)
        {
            return Ok(await _orderService.GetOrderPastDuesById(orderRentId));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> PaiedPastDue(int Id)
        {
            var result = await _orderService.ChangePaymentPastDueStatus(Id);
            if (result != null) return BadRequest();

            return Ok(StatusCodes.Status200OK);
        }

        [Authorize]
        [HttpGet("[action]")]
        public async Task<IActionResult> getByPassenger()
        {
            return Ok(await _orderService.GetOrdersByUser());
        }
    }
}
