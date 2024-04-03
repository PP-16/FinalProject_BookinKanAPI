using BookinKanAPI.DTOs;
using BookinKanAPI.DTOs.BookingCarsDTO;
using BookinKanAPI.Models;
using BookinKanAPI.ServicesManage.BookingServiceManage;
using BookinKanAPI.ServicesManage.PaymentServiceManage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookinKanAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly IPaymentService _paymentService;

        public BookingController(IBookingService bookingService,IPaymentService paymentService)
        {
            _bookingService = bookingService;
            _paymentService = paymentService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult>GetBookings()
        {
            return Ok(await _bookingService.GetBooking());
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> GetTotalPriceBookings()
        {
            return Ok(await _bookingService.GetTotalPricesByDateAtBooking());
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetTotalPriceBookingsByMount(int month, int year)
        {
            return Ok(await _bookingService.GetTotalPricesByMountAtBooking(month, year));
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetTotalPriceBookingsByYear(int year)
        {
            return Ok(await _bookingService.GetTotalPricesByYearAtBooking(year));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateBookings(BookingDTO bookingDTO)
        {
            var result = await _bookingService.CreateBooking(bookingDTO);
            if (result == null) return BadRequest(result);
            return Ok(result);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateEmployeeBookings(EmployeeBookingDTO bookingDTO)
        {
            var result = await _bookingService.CreateBookingByEmpolyee(bookingDTO);
            if (result == null) return BadRequest(result);
            return Ok(result);
        }


        //[HttpGet("[action]")]
        //public async Task<IActionResult> checkSeat(string seatNumber, DateTime dateAtBooking, int itineraryId)
        //{
        //    return Ok(await _bookingService.CheckSeat(seatNumber, dateAtBooking, itineraryId));
        //}

        [HttpPost("[action]")]
        public async Task<IActionResult> UpdateStatus(int ID, Status newStatus)
        {
            var result = await _bookingService.UpdateStatusBooking(ID, newStatus);
            if (result != null) return BadRequest(result);
            return Ok(StatusCodes.Status200OK);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> getseat(DateTime date, int itineraryId)
        {
            return Ok(await _bookingService.GetSeatNumbersForDay(date, itineraryId));
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> getseatstatus(DateTime date, int itineraryId)
        {
            return Ok(await _bookingService.GetSeatNumbersForDayWithStatus(date, itineraryId));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> getTop3 ()
        {
            return Ok(await _bookingService.GetTop3Itinerary());
        }

        [Authorize]
        [HttpGet("[action]")]
        public async Task<IActionResult> getByPassenger()
        {
            return Ok(await _bookingService.GetBookingsByUser());
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetById(int ID)
        {
            return Ok(await _bookingService.GetBookingById(ID));
        }

        [HttpDelete("[action]")]
        public async Task<ActionResult> DeleteBooking(int id) 
        {
            var result = await _bookingService.GetByIdAsync(id);
            if (result == null) return NotFound();

            await _bookingService.DeleteBookings(result);

            return Ok(new { status = "Deleted", StatusCodes.Status200OK });
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetPaymentByBookingId(int ID)
        {
            return Ok(await _bookingService.GetBookingPayment(ID));
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> upDateBooking(int ID, DateTime newDate, List<string> newSeatNumbers=null)
        {
              var update = await _bookingService.UpdateDateBooking(ID, newDate, newSeatNumbers);

            if (update != null) return BadRequest(update);
            return Ok(StatusCodes.Status200OK);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Checkin(int Id, bool checkin)
        {
            var result = await _bookingService.ChangeCheckInStatus(Id, checkin);
            if (result != null) return BadRequest();

            return Ok(StatusCodes.Status200OK);
        }



    }
}
