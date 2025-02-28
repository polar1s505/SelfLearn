using backend.Domain.Models;

namespace backend.Application.Abstract
{
    public interface ITokenService
    {
        string GenerateToken(ApplicationUser user);
    }
}
