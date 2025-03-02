using backend.Application.DTOs.Login;
using backend.Application.DTOs.Register;
using Microsoft.AspNetCore.Identity;

namespace backend.Application.Abstract
{
    public interface IUserAuthenticationService
    {
        Task<LoginResult?> LoginAsync(LoginDTO loginDTO);
        Task<IdentityResult> RegisterAsync(RegisterDTO registerDTO);
    }
}
