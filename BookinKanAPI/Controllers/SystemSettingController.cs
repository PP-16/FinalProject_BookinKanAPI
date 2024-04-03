using BookinKanAPI.DTOs;
using BookinKanAPI.ServicesManage.SystemSettingServiceManage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookinKanAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemSettingController : ControllerBase
    {
        private readonly ISystemSettingService _settingService;

        public SystemSettingController(ISystemSettingService settingService)
        {
            _settingService = settingService;
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> GetSystemSetting()
        {
            return Ok(await _settingService.GetSystem());
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetSystemSettingById(int Id)
        {
            return Ok(await _settingService.GetSystemById(Id));
        }



        [HttpPost("[action]")]
        public async Task<IActionResult> CreateUpdateSystem ([FromForm]SystemSettingDTO settingDTO)
        {
            var result = await _settingService.CreateAndUpdateSystem(settingDTO);
            if (result != null) return BadRequest(result);

            return Ok(StatusCodes.Status200OK);
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteSystemSetting(int Id)
        {
            return Ok(await _settingService.DeleteSetting(Id));
        }

    }
}
