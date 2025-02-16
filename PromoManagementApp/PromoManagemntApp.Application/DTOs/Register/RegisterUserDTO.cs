namespace PromoManagemntApp.Application.DTOs.Register
{
    public record RegisterUserDTO(string Name, string Lastname, string Email, 
        string Password, string ConfirmPassword);
}
