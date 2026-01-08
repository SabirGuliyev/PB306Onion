using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnionPronia.Application.DTOs.AppUsers;
using OnionPronia.Application.Interfaces.Services;

namespace OnionPronia.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAuthenticationService _service;

        public AccountsController(IAuthenticationService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm] RegisterDto userDto)
        {
            await _service.RegisterAsync(userDto);

            return Created();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] LoginDto userDto)
        {
           

            return Ok(await _service.LoginAsync(userDto));
        }
    }
}
