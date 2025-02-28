using backend.Application.Abstract;
using backend.Application.DTOs.Stock;
using backend.Application.Queries.Stock;
using backend.Domain.Models;
using backend.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace backend.Infrastructure.Implementations
{
    public class StockRepo : IStockRepo
    {
        private readonly AppDbContext _appDbContext;

        public StockRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await _appDbContext.Stocks.AddAsync(stockModel);
            await _appDbContext.SaveChangesAsync();

            return stockModel;
        }

        public async Task<Stock?> DeleteAsync(Guid id)
        {
            var stockModel = await _appDbContext.Stocks.FirstOrDefaultAsync(s => s.Id == id);

            if(stockModel == null)
            {
                return null;
            }

            _appDbContext.Stocks.Remove(stockModel);
            await _appDbContext.SaveChangesAsync();

            return stockModel;
        }

        public async Task<List<Stock>> GetAllAsync(StockQuery query)
        {
            var stocks = _appDbContext.Stocks.Include(c => c.Comments).AsNoTracking().AsQueryable();

            if(!string.IsNullOrWhiteSpace(query.Symbol))
            {
                stocks = stocks.Where(s => s.Symbol.Contains(query.Symbol.ToUpperInvariant()));
            }

            if (!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                stocks = stocks.Where(s => s.CompanyName.Contains(query.CompanyName));
            }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if(query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = query.IsDescending ? stocks.OrderByDescending(s => s.Symbol) : stocks.OrderBy(s => s.Symbol); 
                }
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await stocks.Skip(skipNumber).Take(query.PageNumber).ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(Guid id)
        {
            return await _appDbContext.Stocks.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Stock?> UpdateAsync(Guid id, UdpateStockRequestDTO updateDTO)
        {
            var stockModel = await _appDbContext.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(s => s.Id == id);

            if(stockModel == null)
            {
                return null;
            }

            stockModel.Symbol = updateDTO.Symbol;
            stockModel.CompanyName = updateDTO.CompanyName;
            stockModel.Purchase = updateDTO.Purchase;
            stockModel.LastDiv = updateDTO.LastDiv;
            stockModel.Industry = updateDTO.Industry;
            stockModel.MarketCap = updateDTO.MarketCap;

            await _appDbContext.SaveChangesAsync();

            return stockModel;
        }

        public async Task<bool> IsExists(Guid id)
        {
            return await _appDbContext.Stocks.AnyAsync(s => s.Id == id);
        }
    }
}
