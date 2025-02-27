namespace PromoManagementPlatform.Infrastructure.Config
{
    public class JwtOptions
    {
        public const string Jwt = nameof(Jwt);
        public string Issuer { get; set; } = null!;
        public string Audience { get; set; } = null!;
        public string Key { get; set; } = null!;
        public int Expires { get; set; }
    }
}
