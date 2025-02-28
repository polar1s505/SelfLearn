using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace backend.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<ApplicationAssemblyReference>();

            return services;
        }
    }
}
