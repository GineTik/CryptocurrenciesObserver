using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.APIModels;
using Domain.Enums;
using Domain.Results;

namespace Application.Interfaces;

public interface ICryptocurrenciesApi
{
    Task<ApiResult<IEnumerable<Coin>>> GetMostPopularCoinsAsync(string? symbolOrName = null, int? limit = 10);
    Task<ApiResult<IEnumerable<Ticket>>> GetCoinTicketsAsync(string id);
    Task<ApiResult<IEnumerable<CoinPriceHistory>>> GetTradesHistoryAsync(string id, TradesHistoryIntervals interval);
    Task<ApiResult<Exchange>> GetFullExchangeInfoAsync(string id);
}