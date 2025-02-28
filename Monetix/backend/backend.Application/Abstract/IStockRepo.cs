using backend.Domain.Models;
using backend.Application.DTOs.Stock;
using backend.Application.Queries.Stock;

namespace backend.Application.Abstract
{
    public interface IStockRepo
    {
        Task<List<Stock>> GetAllAsync(StockQuery query);
        Task<Stock?> GetByIdAsync(Guid id);
        Task<Stock> CreateAsync(Stock stockModel);
        Task<Stock?> UpdateAsync(Guid id, UdpateStockRequestDTO updateDTO);
        Task<Stock?> DeleteAsync(Guid id);
        Task<bool> IsExists(Guid id);
    }
}
