using backend.Application.Abstract;
using backend.Application.DTOs.Stock;
using backend.Application.Mappers;
using backend.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace backend.Infrastructure.Implementations
{
    public class FMPService : IFMPService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<FMPService> _logger;

        public FMPService(HttpClient httpClient, IConfiguration configuration, ILogger<FMPService> logger)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<Stock?> FindStockBySymbolAsync(string symbol)
        {
            try
            {
                var result = await _httpClient.GetAsync($"https://financialmodelingprep.com/api/v3/profile/{symbol}?apikey={_configuration["FMPKey"]}");
                if(result.IsSuccessStatusCode)
                {
                    var content = await result.Content.ReadAsStringAsync();
                    var stocks = JsonSerializer.Deserialize<FMPStock[]>(content);
                    var stock = stocks[0];
                    if(stock != null)
                    {
                        return stock.ToStockFromFMP();
                    }

                    return null;
                }

                return null;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while fetching stock data for symbol: {Symbol}", symbol);
                return null;
            }
        }
    }
}
