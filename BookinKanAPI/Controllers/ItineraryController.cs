using BookinKanAPI.DTOs.BookingCarsDTO;
using BookinKanAPI.ServicesManage.ItineraryServiceManage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookinKanAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItineraryController : ControllerBase
    {
        private readonly IItineraryService _itineraryService;

        public ItineraryController(IItineraryService itineraryService)
        {
            _itineraryService = itineraryService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetItinerary()
        {
            return Ok(await _itineraryService.GetItineraries());
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateAndUpdateItinerary(ItineraryDTO itineraryDTO)
        {
            var result = await _itineraryService.CreateItineraries(itineraryDTO);

            if (result != null) return BadRequest(result);
            return Ok(StatusCodes.Status201Created);

        }
        [HttpDelete("[action]")]
        public async Task<ActionResult> DeleteCarDrivers(int id)
        {
            var result = await _itineraryService.GetByIdAsync(id);
            if (result == null) return NotFound();
            await _itineraryService.DeleteItinerary(result);
            return Ok(new { status = "Deleted", result, StatusCodes.Status200OK });
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> SearchItinerary(string Routename = "")
        {
            return Ok(await _itineraryService.SearchItinerary(Routename));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CheckIsuseItinarary(int Id, bool isuse)
        {
            var result = await _itineraryService.ChangeIsuse(Id, isuse);
            if (result != null) return BadRequest();

            return Ok(StatusCodes.Status200OK);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetById(int ID)
        {
            return Ok(await _itineraryService.GetByIdAsync(ID));
        }

    }
}
