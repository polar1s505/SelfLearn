using Microsoft.AspNetCore.Mvc;
using PromoManagementPlatform.Application.Abstract;
using PromoManagementPlatform.Application.DTOs.Login;
using PromoManagementPlatform.Application.DTOs.Register;

namespace PromoManagementPlatform.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserAuthenticationService _userAuthenticationService;

        public AuthenticationController(IUserAuthenticationService userAuthenticationService) 
            => _userAuthenticationService = userAuthenticationService;

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterUserDTO registerUserDTO)
        {
            var result = await _userAuthenticationService.RegisterAsync(registerUserDTO);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginUserDTO loginUserDTO)
        {
            var result = await _userAuthenticationService.LoginAsync(loginUserDTO);
            if (!result.IsSuccessful)
            {
                return Unauthorized(result.Errors);
            }
            return Ok(result.Token);
        }
    }
}
