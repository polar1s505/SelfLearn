using backend.Application.Abstract;
using backend.Application.Implementations;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace backend.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<ApplicationAssemblyReference>();

            services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();

            return services;
        }
    }
}
