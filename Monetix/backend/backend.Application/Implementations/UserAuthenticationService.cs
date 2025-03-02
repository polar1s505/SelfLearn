using backend.Application.Abstract;
using backend.Application.DTOs.Login;
using backend.Application.DTOs.Register;
using backend.Domain.Constants;
using backend.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace backend.Application.Implementations
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenService _tokenService;

        public UserAuthenticationService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task<LoginResult?> LoginAsync(LoginDTO loginDTO)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == loginDTO.Username);

            if(user == null)
            {
                return null;
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, false);

            if(!result.Succeeded)
            {
                return null;
            }

            return new LoginResult
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = _tokenService.GenerateToken(user)
            };
        }

        public async Task<IdentityResult> RegisterAsync(RegisterDTO registerDTO)
        {
            try
            {
                var appUser = new ApplicationUser
                {
                    UserName = registerDTO.Username,
                    Email = registerDTO.Email
                };

                var createdUser = await _userManager.CreateAsync(appUser, registerDTO.Password);

                if(createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, UserRoles.User);

                    if(roleResult.Succeeded)
                    {
                        return IdentityResult.Success;
                    }
                    else
                    {
                        return IdentityResult.Failed(new IdentityError { Code = "500", Description = "Failed to add to role" }); 
                    }
                }
                else
                {
                    return IdentityResult.Failed(new IdentityError { Code = "500", Description = "Failed to create a new user" });
                }
            }
            catch (Exception e)
            {

                return IdentityResult.Failed(new IdentityError { Code = "500", Description = "Unexpected error occured!"});
            }
        }

    }
}
