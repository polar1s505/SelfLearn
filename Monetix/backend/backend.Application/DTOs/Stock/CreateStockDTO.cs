namespace backend.Application.DTOs.Stock
{
    public record CreateStockDTO(
        string Symbol,
        string CompanyName,
        decimal Purchase,
        decimal LastDiv,
        string Industry,
        long MarketCap);
}
