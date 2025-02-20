using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PromoManagementPlatform.Application.Abstract;
using PromoManagementPlatform.Application.BackgroundServices;
using PromoManagementPlatform.Domain.Abstract;
using PromoManagementPlatform.Domain.Entities;
using PromoManagementPlatform.Infrastructure.Config;
using PromoManagementPlatform.Infrastructure.Implementations;
using PromoManagementPlatform.Infrastructure.Persistence;
using System.Text;

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
            
            services.AddAuthorization();

            services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.Jwt));
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    RequireExpirationTime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"] ?? ""))
                };
            });

            


            services.AddScoped<IUserManagerWrapper<ApplicationUser>, UserManagerWrapper<ApplicationUser>>();
            services.AddScoped<ITokenGeneratorService, TokenGeneratorService>();
            services.AddScoped<ICampaignRepository, CampaignRepository>();
            services.AddScoped<UpdateCampaignStatusService>();

            RecurringJob.AddOrUpdate("status-check-update", () =>
                services.BuildServiceProvider().GetRequiredService<UpdateCampaignStatusService>().UpdateCampaignStatusAsync(),
                Cron.Minutely());

            return services;
        }
    }
}
