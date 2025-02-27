using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PromoManagementPlatform.Domain.Entities;

namespace PromoManagementPlatform.Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Campaign> Campaigns => Set<Campaign>();
        public DbSet<CampaignMetrics> CampaignMetrics => Set<CampaignMetrics>();
    }
}
