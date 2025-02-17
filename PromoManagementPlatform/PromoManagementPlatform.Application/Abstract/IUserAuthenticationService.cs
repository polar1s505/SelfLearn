using Microsoft.AspNetCore.Identity;
using PromoManagementPlatform.Application.DTOs.Login;
using PromoManagementPlatform.Application.DTOs.Register;

namespace PromoManagementPlatform.Application.Abstract
{
    public interface IUserAuthenticationService
    {
        Task<IdentityResult> RegisterAsync(RegisterUserDTO registerUserDTO);
        Task<IdentityResult> LoginAsync(LoginUserDTO loginUserDTO);
    }
}
