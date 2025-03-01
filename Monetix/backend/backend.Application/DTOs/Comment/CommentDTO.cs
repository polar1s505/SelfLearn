namespace backend.Application.DTOs.Comment
{
    public record CommentDTO(
    Guid Id,
    string Title,
    string Content,
    DateTime CreatedOn,
    string CreatedBy,
    Guid? StockID);
}
