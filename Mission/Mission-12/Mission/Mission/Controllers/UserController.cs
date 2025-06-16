using Microsoft.AspNetCore.Mvc;
using Mission.Entities;
using Mission.Services;
using Mission.Services.IServices;

namespace Mission.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [HttpDelete]
        [Route("DeleteUser/{id}")]
        public ActionResult DeleteUser(int id)
        {
            try
            {
                var res = _userService.UserDelete(id);
                return Ok(new ResponseResult() { Data = res, Result = ResponseStatus.Success, Message = "Deleted" });
            }
            catch
            {
                return BadRequest(new ResponseResult() { Data = null, Result = ResponseStatus.Error, Message = "Failed to delete user" });
            }
        }

        [HttpGet]
        [Route("GetUserById")]
        public async Task<IActionResult> GetUserById([FromQuery] int id)
        {
            try
            {
                var res = await _userService.GetUserById(id);
                return Ok(new ResponseResult() { Data = res, Result = ResponseStatus.Success, Message = "" });
            }
            catch
            {
                return BadRequest(new ResponseResult() { Data = null, Result = ResponseStatus.Error, Message = "Failed to find user." });
            }
        }

        [HttpGet]
        [Route("UserDetailList")]
        public async Task<IActionResult> GetAllUsers()
        {
            var res = await _userService.GetAllUsers();
            return Ok(new ResponseResult() { Data = res, Result = ResponseStatus.Success, Message = "" });
        }
    }
}
