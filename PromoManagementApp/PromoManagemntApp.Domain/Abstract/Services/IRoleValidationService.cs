namespace PromoManagemntApp.Domain.Abstract.Services
{
    public interface IRoleValidationService
    {
        bool IsRoleAllowed(string role);
    }
}
