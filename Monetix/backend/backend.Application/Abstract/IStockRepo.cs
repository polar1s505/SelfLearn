using backend.Domain.Models;
using backend.Application.DTOs.Stock;

namespace backend.Application.Abstract
{
    public interface IStockRepo
    {
        Task<List<Stock>> GetAllAsync();
        Task<Stock?> GetByIdAsync(Guid id);
        Task<Stock> CreateAsync(Stock stockModel);
        Task<Stock?> UpdateAsync(Guid id, UdpateStockRequestDTO updateDTO);
        Task<Stock?> DeleteAsync(Guid id);
        Task<bool> IsExists(Guid id);
    }
}
