using backend.Application.Abstract;
using backend.Infrastructure.Implementations;
using backend.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace backend.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("Monetix")));

            services.AddScoped<IStockRepo, StockRepo>();
            services.AddScoped<ICommentRepo, CommentRepo>();

            return services;
        }
    }
}
