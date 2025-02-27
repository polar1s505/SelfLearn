namespace PromoManagementPlatform.Application.DTOs.Register
{
    public record RegisterUserDTO(string FirstName, string Lastname, string Email,
        string Password, string ConfirmPassword);
}
