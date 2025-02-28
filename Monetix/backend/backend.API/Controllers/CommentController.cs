using backend.Application.Abstract;
using backend.Application.DTOs.Comment;
using backend.Application.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.API.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepo _commentRepo;
        private readonly IStockRepo _stockRepo;

        public CommentController(ICommentRepo commentRepo, IStockRepo stockRepo)
        {
            _commentRepo = commentRepo;
            _stockRepo = stockRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _commentRepo.GetAllAsync();

            var response = comments.Select(c => c.ToCommentDTO()).ToList();

            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var comment = await _commentRepo.GetByIdAsync(id);

            if (comment == null) return NotFound();

            return Ok(comment.ToCommentDTO());
        }

        [HttpPost("{stockId:guid}")]
        public async Task<IActionResult> Create([FromRoute] Guid stockId, [FromBody] CreateCommentDTO commentDTO)
        {
            if(!await _stockRepo.IsExists(stockId))
            {
                return BadRequest("Stock does not exist");
            }

            var commentModel = commentDTO.ToCommentFromCreateDTO(stockId);
            await _commentRepo.CreateAsync(commentModel);
            return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentDTO);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateCommentRequestDTO updateCommentDTO)
        {
            var comment = await _commentRepo.UpdateAsync(id, updateCommentDTO);

            if (comment == null) return NotFound();

            return Ok(comment.ToCommentDTO());
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var commentModel = await _commentRepo.DeleteAsync(id);

            if (commentModel == null) return NotFound();

            return NoContent();
        }
    }
}
