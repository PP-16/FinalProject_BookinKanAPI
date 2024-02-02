using BookinKanAPI.DTOs.BookingCarsDTO;
using BookinKanAPI.ServicesManage.RouteCarsServiceManage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookinKanAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteCarsController : ControllerBase
    {
        private readonly IRouteCarsService _routeCarsService;

        public RouteCarsController(IRouteCarsService routeCarsService)
        {
            _routeCarsService = routeCarsService;
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> GetRoute()
        {
            return Ok(await _routeCarsService.GetRouteCars());
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateAndUpdateRouteCar(RouteCarsDTO routeCars)
        {
            var Class = await _routeCarsService.CreateandUpdateRouteCars(routeCars);
            if (Class != null) return BadRequest(Class);

            return Ok(StatusCodes.Status201Created);
        }

        [HttpDelete("[action]")]
        public async Task<ActionResult> DeleteRouteCar(int id)
        {
            var result = await _routeCarsService.GetByIdAsync(id);
            if (result == null) return NotFound();
            await _routeCarsService.DeleteRouteCars(result);
            return Ok(new { status = "Deleted", result, StatusCodes.Status200OK });
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> SearchRouteCar(string Classname = "")
        {
            return Ok(await _routeCarsService.SearchRouteCars(Classname));
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> CheckIsuseRoute(int Id, bool isuse)
        {
            var result = await _routeCarsService.ChangeIsuse(Id, isuse);
            if (result != null) return BadRequest();

            return Ok(StatusCodes.Status200OK);
        }
    }
}
