using backend.Application.DTOs.Comment;
using backend.Application.Queries.Comment;
using backend.Domain.Models;

namespace backend.Application.Abstract
{
    public interface ICommentRepo
    {
        Task<List<Comment>> GetAllAsync(CommentQuery query);
        Task<Comment?> GetByIdAsync(Guid id);
        Task<Comment> CreateAsync(Comment commentModel);
        Task<Comment?> UpdateAsync(Guid id, UpdateCommentRequestDTO requestDTO);
        Task<Comment?> DeleteAsync(Guid id);
    }
}
