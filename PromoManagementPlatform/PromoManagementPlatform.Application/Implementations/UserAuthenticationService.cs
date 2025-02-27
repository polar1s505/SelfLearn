using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PromoManagementPlatform.Application.Abstract;
using PromoManagementPlatform.Application.DTOs.Login;
using PromoManagementPlatform.Application.DTOs.Register;
using PromoManagementPlatform.Domain.Constants;
using PromoManagementPlatform.Domain.Entities;


namespace PromoManagementPlatform.Application.Implementations
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly IUserManagerWrapper<ApplicationUser> _userManager;
        private readonly ITokenGeneratorService _tokenGeneratorService;
        private readonly IMapper _mapper;

        public UserAuthenticationService(IUserManagerWrapper<ApplicationUser> userManager, ITokenGeneratorService tokenGeneratorService, IMapper mapper)
        {
            _userManager = userManager;
            _tokenGeneratorService = tokenGeneratorService;
            _mapper = mapper;
        }

        public async Task<LoginResponseDTO> LoginAsync(LoginUserDTO loginUserDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginUserDTO.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, loginUserDTO.Password))
            {
                return new LoginResponseDTO(IsSuccessful: false, Token: string.Empty, Errors: new List<string> { "Invalid credentials" });
            }

            var roles = await _userManager.GetRolesAsync(user);
            var token = await _tokenGeneratorService.GenerateTokenAsync(user, roles.ToList());

            return new LoginResponseDTO(IsSuccessful: true, Token: token, Errors: null);
        }

        public async Task<IdentityResult> RegisterAsync(RegisterUserDTO registerUserDTO)
        {
            var user = _mapper.Map<ApplicationUser>(registerUserDTO);

            var result = await _userManager.CreateAsync(user, registerUserDTO.Password);

            if (!result.Succeeded)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Registration failed" });
            }

            return result;

        }
    }
}
