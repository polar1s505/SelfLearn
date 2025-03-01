using backend.Application.Abstract;
using backend.Application.DTOs.Comment;
using backend.Application.Mappers;
using backend.Application.Queries.Comment;
using backend.Domain.Models;
using backend.Infrastructure.Extenstions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace backend.API.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepo _commentRepo;
        private readonly IStockRepo _stockRepo;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IFMPService _fmpService;

        public CommentController(ICommentRepo commentRepo, IStockRepo stockRepo,
            UserManager<ApplicationUser> userManager, IFMPService fmpService)
        {
            _commentRepo = commentRepo;
            _stockRepo = stockRepo;
            _userManager = userManager;
            _fmpService = fmpService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] CommentQuery query)
        {
            var comments = await _commentRepo.GetAllAsync(query);

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

        [HttpPost("{symbol:alpha}")]
        public async Task<IActionResult> Create([FromRoute] string symbol, [FromBody] CreateCommentDTO commentDTO)
        {
            var stock = await _stockRepo.GetBySymbolAsync(symbol);

            if(stock == null)
            {
                stock = await _fmpService.FindStockBySymbolAsync(symbol);
                if(stock == null)
                {
                    return BadRequest("Stock does not exist");
                }

                await _stockRepo.CreateAsync(stock);
            }

            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);

            var commentModel = commentDTO.ToCommentFromCreateDTO(stock.Id);
            commentModel.ApplicationUserId = user!.Id;
            await _commentRepo.CreateAsync(commentModel);
            return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDTO());
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
