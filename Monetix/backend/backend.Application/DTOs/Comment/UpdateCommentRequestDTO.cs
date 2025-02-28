namespace backend.Application.DTOs.Comment
{
    public record UpdateCommentRequestDTO(
        string Title,
        string Content);
}
