using PromoManagemntApp.Domain.Abstract.Services;
using PromoManagemntApp.Domain.Constants;

namespace PromoManagemntApp.Application.Implementations
{
    public class RoleValidationService : IRoleValidationService
    {
        public bool IsRoleAllowed(string role) => UserRolesConstants.AllowedRoles.Contains(role);
    }
}
