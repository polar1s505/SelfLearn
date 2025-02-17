namespace PromoManagementPlatform.Application.DTOs.Login
{
    public record LoginResponseDTO(bool IsSuccessful, string? Token, IList<string>? Errors);
}
