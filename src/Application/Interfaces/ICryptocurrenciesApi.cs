using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.APIModels;

namespace Application.Interfaces;

public interface ICryptocurrenciesApi
{
    Task<bool> PingAsync();
    Task<IEnumerable<CoinMarket>> GetMostPopularCoinsAsync();
    Task<CoinWithTickets> GetCoinInfoAsync(string id);
    Task<Exchange> GetFullExchangeInfoAsync(string id);
}