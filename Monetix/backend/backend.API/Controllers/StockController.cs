using Microsoft.AspNetCore.Mvc;
using backend.Application.Mappers;
using backend.Application.Abstract;
using backend.Application.DTOs.Stock;
using backend.Application.Queries.Stock;
using System.Collections.Generic;

namespace backend.API.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockRepo _stockRepo;

        public StockController(IStockRepo stockRepo)
        {
            _stockRepo = stockRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] StockQuery query)
        {
            var stocks = await _stockRepo.GetAllAsync(query);

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