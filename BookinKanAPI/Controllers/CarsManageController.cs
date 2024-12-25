using BookinKanAPI.DTOs;
using BookinKanAPI.DTOs.RentCarsDTO;
using BookinKanAPI.Models;
using BookinKanAPI.ServicesManage.CarsServiceManage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookinKanAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsManageController : ControllerBase
    {
        private readonly IClassCarsService _classCarsService;
        private readonly ICarsService _carsService;

        public CarsManageController(IClassCarsService  classCarsService,ICarsService carsService)
        {
            _classCarsService = classCarsService;
            _carsService = carsService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetClass()
        {
            return Ok(await _classCarsService.GetClasses());
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateAndUpdateClassCar(ClassCarsDTO classCarsDTO)
        {
            var Class = await _classCarsService.CreateandUpdateClass(classCarsDTO);
            if (Class != null) return BadRequest(Class);

            return Ok(StatusCodes.Status201Created);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> DeleteClassCars(int id)
        {
            var result = await _classCarsService.GetByIdAsync(id);
            if (result == null) return NotFound();
            await _classCarsService.DeleteClass(result);
            return Ok(new { status = "Deleted", result,StatusCodes.Status200OK });
        }

        [HttpGet("[action]")]
        public async Task<IActionResult>SearchClass(string Classname = "")
        {
            return Ok(await _classCarsService.SearchClass(Classname));
        }

        //[HttpPost("[action]")]
        //public async Task<IActionResult> CheckIsuseClass(int Id, bool isuse)
        //{
        //    var result = await _classCarsService.ChangeIsuse(Id, isuse);
        //    if (result != null) return BadRequest();

        //    return Ok(StatusCodes.Status200OK);
        //}
        [HttpPost("[action]")]
        public async Task<ActionResult> DeleteImageCars(int id)
        {
            await _carsService.DeleteImageCars(id);
            return Ok(new { status = "Deleted", id, StatusCodes.Status200OK });
        }



        ///-----------------------------CarsManage-----------------------------///

        [HttpGet("[action]")]
        public async Task<IActionResult> GetCar()
        {
            return Ok(await _carsService.GetCars());
        }

        [HttpPost("[action]")] 
        public async Task<IActionResult> CreateAndUpdateCars([FromForm] CarsRequestDTO carsDTO)
        {
            var Class = await _carsService.CreateandUpdateCars(carsDTO);
            if (Class != null) return BadRequest(Class);

            return Ok(StatusCodes.Status201Created);
        }
         
        [HttpPost("[action]")]
        public async Task<ActionResult> DeleteCars(int id)
        {
            var result = await _carsService.GetByIdAsync(id);
            if (result == null) return NotFound();

            await _carsService.DeleteCars(result);

            return Ok(new { status = "Deleted",  StatusCodes.Status200OK });
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> SearchBrandCars(string CarsBrand = "")
        {
            return Ok(await _carsService.SearchCarsBrand(CarsBrand));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetCarForRents()
        {
            return Ok(await _carsService.GetCarForRent());
        }

        [HttpPost("[action]")]
        public async Task<IActionResult>UpdateStatusCars(int ID, StatusCars newStatus)
        {
            var result = await _carsService.UpdateStatusCars(ID, newStatus);
            if (result != null) return BadRequest(result);
            return Ok(StatusCodes.Status200OK);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> UpdateClassInCars(int ID, int ClassID)
        {
            var result = await _carsService.UpdateClassCar(ID, ClassID);
            if (result != null) return BadRequest(result);
            return Ok(StatusCodes.Status200OK);
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> UpdateStatusCarsRent(int ID)
        {
            var result = await _carsService.UpdateStatusRentCars(ID);
            if (result != null) return BadRequest(result);
            return Ok(StatusCodes.Status200OK);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CheckIsuseCar(int Id, bool isuse)
        {
            var result = await _carsService.ChangeIsuse(Id, isuse);
            if (result != null) return BadRequest();

            return Ok(StatusCodes.Status200OK);
        }
    }
}
