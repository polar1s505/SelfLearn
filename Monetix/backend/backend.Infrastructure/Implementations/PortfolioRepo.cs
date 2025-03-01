using backend.Application.Abstract;
using backend.Domain.Models;
using backend.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace backend.Infrastructure.Implementations
{
    public class PortfolioRepo : IPortfolioRepo
    {
        private readonly AppDbContext _appDbContext;

        public PortfolioRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<Stock>> GetUserPortfolioAsync(ApplicationUser user)
        {
            return await _appDbContext.Portfolios.Where(u => u.ApplicationUserId == user.Id)
                .Select(stock => new Stock
                {
                    Id = stock.StockId,
                    Symbol = stock.Stock.Symbol,
                    CompanyName = stock.Stock.CompanyName,
                    Purchase = stock.Stock.Purchase,
                    LastDiv = stock.Stock.LastDiv,
                    Industry = stock.Stock.Industry,
                    MarketCap = stock.Stock.MarketCap
                }).ToListAsync();
        }

        public async Task<Portfolio> CreateAsync(Portfolio portfolio)
        {
            await _appDbContext.Portfolios.AddAsync(portfolio);
            await _appDbContext.SaveChangesAsync();

            return portfolio;
        }

        public async Task<Portfolio?> DeleteAsync(ApplicationUser user, string symbol)
        {
            var portfolio = await _appDbContext.Portfolios.FirstOrDefaultAsync(
                x => x.ApplicationUserId == user.Id && x.Stock.Symbol.ToLower() == symbol.ToLower());

            if (portfolio == null) return null;

            _appDbContext.Portfolios.Remove(portfolio);
            await _appDbContext.SaveChangesAsync();

            return portfolio;
        }
    }
}
