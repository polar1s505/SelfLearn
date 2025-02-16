using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using PromoManagemntApp.Application.Mappings;
using PromoManagemntApp.Domain.Abstract.Wrappers;
using PromoManagemntApp.Application.Implementations;
using PromoManagemntApp.Domain.Entities;
using PromoManagemntApp.Domain.Abstract.Services;


namespace PromoManagemntApp.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationLayerServices(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<ApplicationAssemblyReference>();
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddScoped<IUserManagerWrapper<ApplicationUser>, UserManagerWrapper<ApplicationUser>>();
            services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();

            return services;
        }
    }
}
