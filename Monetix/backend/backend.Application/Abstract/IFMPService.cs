using backend.Domain.Models;

namespace backend.Application.Abstract
{
    public interface IFMPService
    {
        Task<Stock?> FindStockBySymbolAsync(string symbol);
    }
}
