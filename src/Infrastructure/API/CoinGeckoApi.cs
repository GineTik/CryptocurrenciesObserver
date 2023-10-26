using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.APIModels;

namespace Infrastructure.API;

public class CoinGeckoApi : ICryptocurrenciesApi
{
    private readonly HttpClientFactory _httpClientFactory;

    public CoinGeckoApi(HttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public Task<bool> PingAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<CoinMarket>> GetMostPopularCoinsAsync()
    {
        if (await PingAsync() == false)
            throw new HttpRequestException($"Ping returned false");
        
        const string requestUri = "/coins/markets";
        
        using var httpClient = _httpClientFactory.Create();
        var responseMessage = await httpClient.GetAsync(requestUri);

        if (responseMessage.IsSuccessStatusCode == false)
            throw new HttpRequestException($"The response (request {requestUri}) is unsuccessful");

        return (await responseMessage.Content.ReadFromJsonAsync<IEnumerable<CoinMarket>>())!;
    }

    public Task<CoinWithTickets> GetCoinInfoAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<Exchange> GetFullExchangeInfoAsync(string id)
    {
        throw new NotImplementedException();
    }
}