using Microsoft.AspNetCore.Identity;

namespace backend.Domain.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<Portfolio> Portfolios { get; set; } = new List<Portfolio>();
    }
}
