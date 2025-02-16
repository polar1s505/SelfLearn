using Microsoft.AspNetCore.Identity;
using PromoManagemntApp.Application.DTOs.Login;
using PromoManagemntApp.Application.DTOs.Register;

namespace PromoManagemntApp.Domain.Abstract.Services
{
    public interface IUserAuthenticationService
    {
        Task<IdentityResult> RegisterAsync(RegisterUserDTO registerUserDTO);
        Task<IdentityResult> LoginAsync(LoginUserDTO loginUserDTO);
    }
}
