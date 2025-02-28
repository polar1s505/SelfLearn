namespace backend.Application.DTOs.Login
{
    public class LoginResult
    {
        public bool Success { get; set; }
        public string? Token { get; set; } = null;
        public string? ErrorMessage { get; set; } = null;
    }
}
