using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.APIModels;
using Domain.Enums;
using Domain.Results;
using Infrastructure.API.ApiCache;
using Infrastructure.API.ConvertorInterfaces;
using Infrastructure.API.HttpClientFactory;

namespace Infrastructure.API.CoinCap;

public class CoinCapApi : CryptocurrenciesApiBase
{
    private const string GetMostPopularCoinsRequestUri = "/v2/assets";
    private const string GetCoinTicketsRequestUri = "/v2/assets/{id}/markets";
    private const string GetTradesHistoryRequestUri = "/v2/assets/{id}/history";
    private const string GetFullExchangeInfoRequestUri = "/v2/exchanges/{id}";
    
    private readonly ICoinsJsonConvertor _coinsJsonConvertor;
    private readonly ITicketsJsonConvertor _ticketsJsonConvertor;
    private readonly ICoinPriceHistoryJsonConverter _coinPriceHistoryJsonConverter;
    private readonly IExchangeJsonConvertor _exchangeJsonConvertor;

    public CoinCapApi(IHttpClientFactory httpClientFactory, 
        ICoinsJsonConvertor coinsJsonConvertor, 
        ITicketsJsonConvertor ticketsJsonConvertor, 
        ICoinPriceHistoryJsonConverter coinPriceHistoryJsonConverter, 
        IApiResultCache apiResultCache, IExchangeJsonConvertor exchangeJsonConvertor) 
        : base("https://api.coincap.io", httpClientFactory, apiResultCache)
    {
        _coinsJsonConvertor = coinsJsonConvertor;
        _ticketsJsonConvertor = ticketsJsonConvertor;
        _coinPriceHistoryJsonConverter = coinPriceHistoryJsonConverter;
        _exchangeJsonConvertor = exchangeJsonConvertor;
    }
    
    public override async Task<ApiResult<IEnumerable<Coin>>> GetMostPopularCoinsAsync(string? symbolOrName,
        int? limit)
    {
        return await GetAndCacheResultAsync(GetMostPopularCoinsRequestUri, new()
        {
            { "search", symbolOrName },
            { "limit", limit }
        }, _coinsJsonConvertor);
    }

    public override async Task<ApiResult<IEnumerable<Ticket>>> GetCoinTicketsAsync(string id)
    {
        return await GetAndCacheResultAsync(GetCoinTicketsRequestUri, new()
        {
            { "id", id }
        }, _ticketsJsonConvertor);
    }

    public override async Task<ApiResult<IEnumerable<CoinPriceHistory>>> GetTradesHistoryAsync(string id, TradesHistoryIntervals interval)
    {
        return await GetAndCacheResultAsync(GetTradesHistoryRequestUri, new()
        {
            { "id", id },
            { "interval", interval.ToString().ToLower() }
        }, _coinPriceHistoryJsonConverter);
    }

    public override async Task<ApiResult<Exchange>> GetFullExchangeInfoAsync(string id)
    {
        return await GetAndCacheResultAsync(GetFullExchangeInfoRequestUri, new()
        {
            { "id", id }
        }, _exchangeJsonConvertor);
    }
}