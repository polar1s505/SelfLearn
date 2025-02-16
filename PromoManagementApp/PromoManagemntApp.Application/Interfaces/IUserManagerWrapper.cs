using Microsoft.AspNetCore.Identity;

namespace PromoManagemntApp.Domain.Abstract.Wrappers
{
    public interface IUserManagerWrapper<TUser> where TUser : class
    {
        Task<IdentityResult> CreateAsync(TUser user, string password);
        Task<IList<string>> GetRolesAsync(TUser user);
        Task<IdentityResult> AddToRoleAsync(TUser user, string role);
        Task<TUser?> FindByEmailAsync(string email);
        Task<TUser?> FindIdAsync(string id);
        Task<bool> CheckPasswordAsync(TUser user, string password);

    }
}
