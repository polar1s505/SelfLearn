using backend.Application.Abstract;
using backend.Domain.Models;
using backend.Infrastructure.Extenstions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace backend.API.Controllers
{
    [Route("api/portfolio")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IStockRepo _stockRepo;
        private readonly IPortfolioRepo _portfolioRepo;

        public PortfolioController(UserManager<ApplicationUser> userManager, IStockRepo stockRepo, IPortfolioRepo portfolioRepo)
        {
            _userManager = userManager;
            _stockRepo = stockRepo;
            _portfolioRepo = portfolioRepo;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio()
        {
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);
            var userPortfolio = await _portfolioRepo.GetUserPortfolioAsync(user!);

            return Ok(userPortfolio);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPortfolio(string symbol)
        {
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);
            var stock = await _stockRepo.GetBySymbolAsync(symbol.ToUpper());

            if (stock == null) return BadRequest("Stock not found");

            var userPortfolio = await _portfolioRepo.GetUserPortfolioAsync(user!);

            if (userPortfolio.Any(x => x.Symbol.ToLower() == symbol.ToLower())) return BadRequest("Cannot add same stock to portfolio");

            var portfolioModel = new Portfolio
            {
                StockId = stock.Id,
                ApplicationUserId = user!.Id
            };

            var result = await _portfolioRepo.CreateAsync(portfolioModel);

            if (result == null) return StatusCode(500, "Could not create");

            return Created();
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeletePortfolio(string symbol)
        {
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);

            var userPortfolio = await _portfolioRepo.GetUserPortfolioAsync(user!);

            var filteredStock = userPortfolio.Where(s => s.Symbol.ToLower() == symbol.ToLower()).ToList();

            if (filteredStock.Count() == 0) return BadRequest("Stock not found");

            await _portfolioRepo.DeleteAsync(user!, symbol);

            return NoContent();
        }
    }
}
