namespace backend.Application.DTOs.Stock
{
    public record StockDTO(
        Guid Id,
        string Symbol,
        string CompanyName,
        decimal Purchase,
        decimal LastDiv,
        string Industry,
        long MarketCap);
}
