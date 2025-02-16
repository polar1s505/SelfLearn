using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PromoManagemntApp.Domain.Entities;

namespace PromoManagemntApp.Infrastructure.Persistence
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
