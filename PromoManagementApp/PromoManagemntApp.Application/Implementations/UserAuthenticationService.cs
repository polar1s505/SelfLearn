using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PromoManagemntApp.Application.DTOs.Login;
using PromoManagemntApp.Application.DTOs.Register;
using PromoManagemntApp.Domain.Abstract.Services;
using PromoManagemntApp.Domain.Abstract.Wrappers;
using PromoManagemntApp.Domain.Entities;

namespace PromoManagemntApp.Application.Implementations
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly IUserManagerWrapper<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public UserAuthenticationService(IUserManagerWrapper<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IdentityResult> LoginAsync(LoginUserDTO loginUserDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginUserDTO.Email);

            if(user == null || await _userManager.CheckPasswordAsync(user, loginUserDTO.Password))
            {
                return IdentityResult.Failed(new IdentityError { Description = "Invalid credentials" });
            }

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterUserDTO registerUserDTO)
        {
            var user = _mapper.Map<ApplicationUser>(registerUserDTO);

            var result = await _userManager.CreateAsync(user, registerUserDTO.Password);

            if (!result.Succeeded)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Registration failed"});
            }

            return result;

        }
    }
}
