using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.APIModels;
using Domain.Results;
using Infrastructure.API.ConvertorInterfaces;

namespace Infrastructure.API;

public class CoinCupApi : ICryptocurrenciesApi
{
    private const string GetMostPopularCoinsRequestUri = "/v2/assets";
    
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ICoinsJsonConvertor _coinsJsonConvertor;

    public CoinCupApi(IHttpClientFactory httpClientFactory, ICoinsJsonConvertor coinsJsonConvertor)
    {
        _httpClientFactory = httpClientFactory;
        _coinsJsonConvertor = coinsJsonConvertor;
        _httpClientFactory.SetBaseUri("https://api.coincap.io");
    }
    
    public async Task<ApiResult<IEnumerable<Coin>>> GetMostPopularCoinsAsync(string? symbolOrName,
        int? limit = 10)
    {
        using var httpClient = _httpClientFactory.Create();
        var responseMessage = await httpClient.GetAsync(GetMostPopularCoinsRequestUri + $"?search={symbolOrName}&limit={limit}");

        if (responseMessage.IsSuccessStatusCode == false)
            return ApiResult.Exception<IEnumerable<Coin>>(responseMessage);
        
        var json = await responseMessage.Content.ReadAsStringAsync();
        return ApiResult.Content(_coinsJsonConvertor.Convert(json));
    }

    public Task<ApiResult<CoinShortWithTickets>> GetCoinInfoAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResult<Exchange>> GetFullExchangeInfoAsync(string id)
    {
        throw new NotImplementedException();
    }
}