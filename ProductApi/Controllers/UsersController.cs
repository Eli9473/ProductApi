using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.App.Contract.Dto;
using Product.App.Contract.IServices;

namespace ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto dto)
        {
            var token = userService.Login(dto);
            return Ok(token);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var id = await userService.Register(dto);
            return Ok(id);
        }
    }
}
