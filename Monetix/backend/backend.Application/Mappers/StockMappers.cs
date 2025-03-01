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
                MarketCap: stockModel.MarketCap,
                Comments: stockModel.Comments.Select(c => c.ToCommentDTO()).ToList()
            );
        }

        public static Stock ToStockFromCreateDTO(this CreateStockDTO requestDTO)
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

        public static Stock ToStockFromFMP(this FMPStock fMPStock)
        {
            return new Stock
            {
                Symbol = fMPStock.symbol,
                CompanyName = fMPStock.companyName,
                Purchase = (decimal)fMPStock.price,
                LastDiv = (decimal)fMPStock.lastDiv,
                Industry = fMPStock.industry,
                MarketCap = fMPStock.mktCap
            };
        }
    }
}
