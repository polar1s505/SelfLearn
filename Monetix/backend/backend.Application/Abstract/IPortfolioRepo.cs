using backend.Domain.Models;

namespace backend.Application.Abstract
{
    public interface IPortfolioRepo
    {
        Task<List<Stock>> GetUserPortfolioAsync(ApplicationUser user);
        Task<Portfolio> CreateAsync(Portfolio portfolio);
        Task<Portfolio?> DeleteAsync(ApplicationUser user, string symbol);
    }
}
