using BooksApi.Data.Models;
using BooksApi.Services;
using BooksApi.Services.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BooksApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("all")]
        public async Task<IActionResult> GetAllUsers([FromBody] FilterVM filterRequest)
        {
            var data = await _userService.GetAllUsers(filterRequest);
            return Ok(data);
        }

    }
}
