using BookinKanAPI.DTOs.RentCarsDTO;
using BookinKanAPI.ServicesManage.DriverServiceManage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookinKanAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriversController : ControllerBase
    {
        private readonly IDriverService _driverService;

        public DriversController(IDriverService driverService)
        {
            _driverService = driverService;
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> GetDriver()
        {
            return Ok(await _driverService.GetDrivers());
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateandUpdateDriver(DriverDTO driverDTO)
        {
            var Class = await _driverService.CreateandUpdateDrivers(driverDTO);
            if (Class != null) return BadRequest(Class);

            return Ok(StatusCodes.Status201Created);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> DeleteCarDrivers(int id)
        {
            var result = await _driverService.GetByIdAsync(id);
            if (result == null) return NotFound();
            await _driverService.DeleteCars(result);
            return Ok(new { status = "Deleted", result, StatusCodes.Status200OK });
        }

        [HttpGet("[action]")] 
        public async Task<IActionResult> SearchDrivers(string Classname = "")
        {
            return Ok(await _driverService.SearchCarsDriver(Classname));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> SearchByCharges(int minCharges, int maxCharges)
        {
            return Ok(await _driverService.searchDriverByCharges(minCharges,maxCharges));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CheckIsuseDriver(int Id, bool isuse)
        {
            var result = await _driverService.ChangeIsuse(Id, isuse);
            if (result != null) return BadRequest();

            return Ok(StatusCodes.Status200OK);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> updateStatusDriver(int Id, int newStatus)
        {
            var result = await _driverService.UpdateStatusDriver(Id, newStatus);
            if (result != null) return BadRequest();

            return Ok(StatusCodes.Status200OK);
        }
    }
}
