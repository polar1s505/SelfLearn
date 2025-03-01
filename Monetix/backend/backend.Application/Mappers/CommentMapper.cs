using backend.Application.DTOs.Comment;
using backend.Domain.Models;

namespace backend.Application.Mappers
{
    public static class CommentMapper
    {
        public static CommentDTO ToCommentDTO(this Comment commentModel)
        {
            return new CommentDTO
                (
                    Id: commentModel.Id,
                    Title: commentModel.Title,
                    Content: commentModel.Content,
                    CreatedOn: commentModel.CreatedOn,
                    CreatedBy: commentModel.ApplicationUser.UserName,
                    StockID: commentModel.StockID
                );
        }

        public static Comment ToCommentFromCreateDTO(this CreateCommentDTO createCommentDTO, Guid stockId)
        {
            return new Comment
            {
                Title = createCommentDTO.Title,
                Content = createCommentDTO.Content,
                StockID = stockId
            };
        }
    }
}
