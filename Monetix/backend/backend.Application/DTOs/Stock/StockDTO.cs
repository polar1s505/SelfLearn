using backend.Application.DTOs.Comment;

namespace backend.Application.DTOs.Stock
{
    public record StockDTO(
        Guid Id,
        string Symbol,
        string CompanyName,
        decimal Purchase,
        decimal LastDiv,
        string Industry,
        long MarketCap,
        List<CommentDTO> Comments);
}
