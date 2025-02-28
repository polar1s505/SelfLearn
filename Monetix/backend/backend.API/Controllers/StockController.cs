using Microsoft.AspNetCore.Mvc;
using backend.Application.Mappers;
using backend.Application.Abstract;
using backend.Application.DTOs.Stock;

namespace backend.API.Controllers
{
    [Route("api/stocks")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockRepo _stockRepo;

        public StockController(IStockRepo stockRepo)
        {
            _stockRepo = stockRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var stocks = await _stockRepo.GetAllAsync();

            var response = stocks.Select(s => s.ToStockDTO()).ToList();

            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var response = await _stockRepo.GetByIdAsync(id);

            if (response == null) return NotFound();

            return Ok(response.ToStockDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockDTO updateDTO)
        {
            var stockModel = updateDTO.ToStockFromCreateDTO();

            await _stockRepo.CreateAsync(stockModel);

            return CreatedAtAction(nameof(GetById), new {id = stockModel.Id}, stockModel.ToStockDTO());
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UdpateStockRequestDTO udpateDTO)
        {
            var stockModel = await _stockRepo.UpdateAsync(id, udpateDTO);

            if (stockModel == null) return NotFound();

            return Ok(stockModel.ToStockDTO());
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var stockModel = await _stockRepo.DeleteAsync(id);

            if (stockModel == null) return NotFound();

            return NoContent();
        }
    }
}
