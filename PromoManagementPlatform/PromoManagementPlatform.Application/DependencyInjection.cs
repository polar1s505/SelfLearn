using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using PromoManagementPlatform.Application.Mappings;
using PromoManagementPlatform.Application.Implementations;
using PromoManagementPlatform.Application.Abstract;

namespace PromoManagementPlatform.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationLayerServices(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<ApplicationAssemblyReference>();
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();

            return services;
        }
    }
}
