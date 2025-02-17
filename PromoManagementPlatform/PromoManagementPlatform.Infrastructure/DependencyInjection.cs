using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PromoManagementPlatform.Application.Abstract;
using PromoManagementPlatform.Domain.Entities;
using PromoManagementPlatform.Infrastructure.Implementations;
using PromoManagementPlatform.Infrastructure.Persistence;

namespace PromoManagementPlatform.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureLayerServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("Database")));
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IUserManagerWrapper<ApplicationUser>, UserManagerWrapper<ApplicationUser>>();

            return services;
        }
    }
}
