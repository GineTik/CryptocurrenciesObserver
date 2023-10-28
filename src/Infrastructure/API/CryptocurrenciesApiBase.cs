using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.APIModels;
using Domain.Results;

namespace Infrastructure.API;

public abstract class CryptocurrenciesApiBase : ICryptocurrenciesApi
{
    private readonly IHttpClientFactory _httpClientFactory;
    
    protected CryptocurrenciesApiBase(string baseUri, IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
        _httpClientFactory.SetBaseUri(baseUri);
    }

    public abstract Task<ApiResult<IEnumerable<Coin>>> GetMostPopularCoinsAsync(string? symbolOrName, int? limit);

    public abstract Task<ApiResult<CoinShortWithTickets>> GetCoinInfoAsync(string id);
    
    public abstract Task<ApiResult<Exchange>> GetFullExchangeInfoAsync(string id);

    protected async Task<HttpResponseMessage> GetAsync(string uri, Dictionary<string, object?>? parameters = null)
    {
        using var httpClient = _httpClientFactory.Create();
        var parametersAsString = parameters == null ? "" : "?" + String.Join("&", parameters
                .Where(p => p.Value != null)
                .Select(p => p.Key + "=" + p.Value));
        return await httpClient.GetAsync(uri + parametersAsString);
    }
}