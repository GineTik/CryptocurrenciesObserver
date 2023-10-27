using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.APIModels;
using Domain.Results;

namespace Application.Interfaces;

public interface ICryptocurrenciesApi
{
    Task<ApiResult<IEnumerable<Coin>>> GetMostPopularCoinsAsync(string? symbolOrName = null, int? limit = 10);
    Task<ApiResult<CoinShortWithTickets>> GetCoinInfoAsync(string id);
    Task<ApiResult<Exchange>> GetFullExchangeInfoAsync(string id);
}