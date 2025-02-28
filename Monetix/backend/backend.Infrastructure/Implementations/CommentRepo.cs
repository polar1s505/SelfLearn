using backend.Application.Abstract;
using backend.Application.DTOs.Comment;
using backend.Domain.Models;
using backend.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace backend.Infrastructure.Implementations
{
    public class CommentRepo : ICommentRepo
    {
        private readonly AppDbContext _appDbContext;

        public CommentRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _appDbContext.Comments.ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(Guid id)
        {
            return await _appDbContext.Comments.FirstOrDefaultAsync(c => c.Id == id);
        }


        public async Task<Comment> CreateAsync(Comment commentModel)
        {
            await _appDbContext.AddAsync(commentModel);
            await _appDbContext.SaveChangesAsync();
            return commentModel;
        }

        public async Task<Comment?> DeleteAsync(Guid id)
        {
            var comment = await _appDbContext.Comments.FirstOrDefaultAsync(c => c.Id == id);

            if (comment == null) return null;

            _appDbContext.Comments.Remove(comment);
            await _appDbContext.SaveChangesAsync();

            return comment;
        }

        public async Task<Comment?> UpdateAsync(Guid id, UpdateCommentRequestDTO requestDTO)
        {
            var comment = await _appDbContext.Comments.FirstOrDefaultAsync(c => c.Id == id);

            if (comment == null) return null;

            comment.Title = requestDTO.Title;
            comment.Content = requestDTO.Content;
            await _appDbContext.SaveChangesAsync();

            return comment;
        }
    }
}
