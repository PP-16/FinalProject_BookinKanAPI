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
            return Ok(await _orderService.GetOrders());
        }

        //[HttpPost("[action]")]
        //public async Task<IActionResult> CreateUpdateOrderRent(OrderRentDTO rentDTO)
        //{
        //    var result = await _orderService.CreateAndUpdateOrders(rentDTO);
        //    if (result != null) return BadRequest(result) ;

        //    return Ok(StatusCodes.Status201Created);
        //}

        [HttpGet("[action]")]
        public async Task<IActionResult> GetOrderItem()
        {
            return Ok(await _orderService.GetOrdersItems());
        }

        //[HttpPost("[action]")]
        //public async Task<IActionResult> CreateUpdateOrderRentItem(OrderRentItemDTO itemDTO)
        //{
        //    var result = await _orderService.CreateAndUpdateOrdersItems(itemDTO);
        //    if (result != null) return BadRequest(result);

        //    return Ok(StatusCodes.Status201Created);
        //}

        //[Authorize]
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateOrders(OrderRentItemDTO request)
        {
            var result = await _orderService.CreateOrderRent(request);
            if (result != null) return BadRequest(result);

            return Ok(StatusCodes.Status201Created);

        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetcarInRented()
        {
            return Ok(await _orderService.GetRentedCarsNow());
        }
    }
}
