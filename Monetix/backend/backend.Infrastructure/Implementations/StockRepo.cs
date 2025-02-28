using backend.Application.Abstract;
using backend.Application.DTOs.Stock;
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

        public async Task<List<Stock>> GetAllAsync()
        {
            return await _appDbContext.Stocks.ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(Guid id)
        {
            return await _appDbContext.Stocks.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Stock?> UpdateAsync(Guid id, UdpateStockRequestDTO updateDTO)
        {
            var stockModel = await _appDbContext.Stocks.FirstOrDefaultAsync(s => s.Id == id);

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
    }
}
