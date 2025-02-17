using Microsoft.AspNetCore.Identity;
using PromoManagementPlatform.Application.Abstract;

namespace PromoManagementPlatform.Infrastructure.Implementations
{
    public class UserManagerWrapper<TUser> : IUserManagerWrapper<TUser> where TUser : class
    {
        private readonly UserManager<TUser> _userManager;

        public UserManagerWrapper(UserManager<TUser> userManager)
        {
            _userManager = userManager;
        }

        public Task<IdentityResult> CreateAsync(TUser user, string password)
            => _userManager.CreateAsync(user, password);
        public Task<IList<string>> GetRolesAsync(TUser user)
            => _userManager.GetRolesAsync(user);
        public Task<IdentityResult> AddToRoleAsync(TUser user, string role)
            => _userManager.AddToRoleAsync(user, role);
        public Task<TUser?> FindByEmailAsync(string email)
            => _userManager.FindByEmailAsync(email);
        public Task<TUser?> FindIdAsync(string id)
            => _userManager.FindByIdAsync(id);
        public Task<bool> CheckPasswordAsync(TUser user, string password)
            => _userManager.CheckPasswordAsync(user, password);
    }
}
