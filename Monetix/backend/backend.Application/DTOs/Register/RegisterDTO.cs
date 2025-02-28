namespace backend.Application.DTOs.Register
{
    public record RegisterDTO(
        string Username,
        string Email,
        string Password,
        string ConfirmPassword);
}
