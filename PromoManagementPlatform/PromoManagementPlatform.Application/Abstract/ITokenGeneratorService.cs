using PromoManagementPlatform.Domain.Entities;

namespace PromoManagementPlatform.Application.Abstract
{
     public interface ITokenGeneratorService
    {
        Task<string> GenerateTokenAsync(ApplicationUser user, List<string> userRoles);
    }
}
