using backend.Application.Abstract;
using backend.Application.DTOs.Login;
using backend.Application.DTOs.Register;
using Microsoft.AspNetCore.Mvc;

namespace backend.API.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserAuthenticationService _userAuthenticationService;

        public AccountController(IUserAuthenticationService userAuthenticationService)
        {
            _userAuthenticationService = userAuthenticationService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            var result = await _userAuthenticationService.RegisterAsync(registerDTO);

            if(!result.Succeeded)
            {
                return StatusCode(500, result.Errors);
            }

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var result = await _userAuthenticationService.LoginAsync(loginDTO);

            if(result == null)
            {
                return Unauthorized();
            }

            return Ok(result);
        }
    }
}
