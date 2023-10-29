using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.APIModels;
using Domain.Results;
using Infrastructure.API.ConvertorInterfaces;

namespace Infrastructure.API.CoinCap;

public class CoinCapApiBase : CryptocurrenciesApiBase
{
    private const string GetMostPopularCoinsRequestUri = "/v2/assets";
    private const string GetCoinTicketsRequestUri = "/v2/assets/{id}/markets";
    
    private readonly ICoinsJsonConvertor _coinsJsonConvertor;
    private readonly ITicketsJsonConvertor _ticketsJsonConvertor;

    public CoinCapApiBase(IHttpClientFactory httpClientFactory, ICoinsJsonConvertor coinsJsonConvertor, ITicketsJsonConvertor ticketsJsonConvertor) 
        : base("https://api.coincap.io", httpClientFactory)
    {
        _coinsJsonConvertor = coinsJsonConvertor;
        _ticketsJsonConvertor = ticketsJsonConvertor;
    }
    
    public override async Task<ApiResult<IEnumerable<Coin>>> GetMostPopularCoinsAsync(string? symbolOrName,
        int? limit)
    {
        return await GetAndHandleResultAsync(GetMostPopularCoinsRequestUri, new()
        {
            { "search", symbolOrName },
            { "limit", limit }
        }, _coinsJsonConvertor);
    }

    public override async Task<ApiResult<IEnumerable<Ticket>>> GetCoinTicketsAsync(string id)
    {
        return await GetAndHandleResultAsync(GetCoinTicketsRequestUri, new()
        {
            { "id", id }
        }, _ticketsJsonConvertor);
    }

    public override Task<ApiResult<Exchange>> GetFullExchangeInfoAsync(string id)
    {
        throw new NotImplementedException();
    }
}