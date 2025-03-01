using backend.Application.Abstract;
using backend.Application.DTOs.Comment;
using backend.Application.Queries.Comment;
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

        public async Task<List<Comment>> GetAllAsync(CommentQuery query)
        {
            var comments = _appDbContext.Comments.Include(u => u.ApplicationUser)
                .AsNoTracking().AsQueryable();
                
            if(!string.IsNullOrWhiteSpace(query.Symbol))
            {
                comments = comments.Where(s => s.Stock.Symbol.ToLower() == query.Symbol.ToLower());
            }

            if(query.IsDescending)
            {
                comments = comments.OrderByDescending(c => c.CreatedOn);
            }

            return await comments.ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(Guid id)
        {
            return await _appDbContext.Comments.Include(u => u.ApplicationUser).FirstOrDefaultAsync(c => c.Id == id);
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
