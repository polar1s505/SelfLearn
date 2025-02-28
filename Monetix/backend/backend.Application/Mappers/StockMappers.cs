using backend.Application.DTOs.Stock;
using backend.Domain.Models;

namespace backend.Application.Mappers
{
    public static class StockMappers
    {
        public static StockDTO ToStockDTO(this Stock stockModel)
        {
            return new StockDTO
            (
                Id: stockModel.Id,
                Symbol: stockModel.Symbol,
                CompanyName: stockModel.CompanyName,
                Purchase: stockModel.Purchase,
                LastDiv: stockModel.LastDiv,
                Industry: stockModel.Industry,
                MarketCap: stockModel.MarketCap
            );
        }

        public static Stock ToStockFromCreateCommand(this CreateStockDTO requestDTO)
        {
            return new Stock
            {
                Symbol = requestDTO.Symbol,
                CompanyName = requestDTO.CompanyName,
                Purchase = requestDTO.Purchase,
                LastDiv = requestDTO.LastDiv,
                Industry = requestDTO.Industry,
                MarketCap = requestDTO.MarketCap
            };
        }
    }
}
