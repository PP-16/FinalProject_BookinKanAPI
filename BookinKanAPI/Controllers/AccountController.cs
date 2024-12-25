using BookinKanAPI.DTOs.AuthenDto;
using BookinKanAPI.ServicesManage.AuthenServiceManage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BookinKanAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenService _service;

        public AccountController(IAuthenService service)
        {
            _service = service;
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllUser()
        {
            var result = await _service.GetAllAccounts();
            return Ok(result);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAdmins()
        {
            var result = await _service.GetAdmin();
            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> RegisterAdmins(RegisterDTO request)
        {
            var user = await _service.RegisterAdmin(request);

            if (user == null) return BadRequest("User already exists or Invalid email format or Password wrong");

            return Ok(user);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register(RegisterDTO request)
        {
            var user = await _service.Register(request);

            if (user == null) return BadRequest("User already exists or Invalid email format or Password wrong");

            return Ok(user);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginDTO request)
        {
            var passenger = await _service.Login(request);
            if (passenger == null) return BadRequest();
            return Ok(passenger);
        }
        [Authorize]
        [HttpPost("[action]")]
        public async Task<IActionResult> ChangePassword( string NewPass, string checkNewPass)
        {
            var result = await _service.ChangePassword(NewPass, checkNewPass);
            if (result != null) return BadRequest(result);

            return Ok(StatusCodes.Status200OK);
        }


       
        [HttpPost("[action]")]
        public async Task<IActionResult> GetUserInfoFromToken(string token)
        {
            var user = await _service.ValidateToken(token);

            return Ok(user);
        }
        [Authorize]
        [HttpPost("[action]")]
        public async Task<IActionResult> CheckOldpassword(string oldPass)
        {
            var result = await _service.CheckOldPass(oldPass);
            if (result != null) return BadRequest();

            return Ok(StatusCodes.Status200OK);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CheckIsuse(int Id, bool isuse)
        {
            var result = await _service.ChangeIsuse(Id,isuse);
            if (result != null) return BadRequest();

            return Ok(StatusCodes.Status200OK);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> ChangeRoles(int PassId, int RoleId)
        {
            var result = await _service.ChangeRole(PassId, RoleId);
            if (result != null) return BadRequest();

            return Ok(StatusCodes.Status200OK);
        }

        //[Authorize]
        [HttpPost("[action]")]
        public async Task<IActionResult> updateProfileuser([FromForm] UploadImageDTO uploadImageDTO)
        {
            var result = await _service.UploadImageUser(uploadImageDTO);
            if (result != null) return BadRequest();

            return Ok(StatusCodes.Status200OK);
        }

        //**********************************************Role***************************************************************************************************//

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateNewRoles(RoleDTO roleDTO)
        {
            var result = await _service.createAndUpdateRole(roleDTO);
            if (result != null) return BadRequest();

            return Ok(StatusCodes.Status200OK);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetRols()
        {
            return Ok(await _service.getRole());
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> DeleteRole(int id)
        {
            var result = await _service.GetRoleByIdAsync(id);
            if (result == null) return NotFound();
            await _service.DeleteRole(result);
            return Ok(new { status = "Deleted", result, StatusCodes.Status200OK });
        }
        //[HttpPost("[action]")]
        //public async Task<IActionResult> CheckIsuseRoles(int Id, bool isuse)
        //{
        //    var result = await _service.ChangeIsuseRole(Id, isuse);
        //    if (result != null) return BadRequest();

        //    return Ok(StatusCodes.Status200OK);
        //}

    }
}
