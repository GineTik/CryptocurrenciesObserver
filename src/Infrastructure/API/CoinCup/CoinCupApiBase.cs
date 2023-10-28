using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.APIModels;
using Domain.Results;
using Infrastructure.API.ConvertorInterfaces;

namespace Infrastructure.API.CoinCup;

public class CoinCupApiBase : CryptocurrenciesApiBase
{
    private const string GetMostPopularCoinsRequestUri = "/v2/assets";
    
    private readonly ICoinsJsonConvertor _coinsJsonConvertor;

    public CoinCupApiBase(IHttpClientFactory httpClientFactory, ICoinsJsonConvertor coinsJsonConvertor) 
        : base("https://api.coincap.io", httpClientFactory)
    {
        _coinsJsonConvertor = coinsJsonConvertor;
    }
    
    public override async Task<ApiResult<IEnumerable<Coin>>> GetMostPopularCoinsAsync(string? symbolOrName,
        int? limit)
    {
        var responseMessage = await GetAsync(GetMostPopularCoinsRequestUri, new Dictionary<string, object?>
        {
            { "search", symbolOrName },
            { "limit", limit }
        });

        if (responseMessage.IsSuccessStatusCode == false)
            return ApiResult.Exception<IEnumerable<Coin>>(responseMessage);
        
        var json = await responseMessage.Content.ReadAsStringAsync();
        return ApiResult.Content(_coinsJsonConvertor.Convert(json));
    }

    public override Task<ApiResult<CoinShortWithTickets>> GetCoinInfoAsync(string id)
    {
        throw new NotImplementedException();
    }

    public override Task<ApiResult<Exchange>> GetFullExchangeInfoAsync(string id)
    {
        throw new NotImplementedException();
    }
}