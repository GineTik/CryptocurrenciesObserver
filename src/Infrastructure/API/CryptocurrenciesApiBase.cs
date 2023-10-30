using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.APIModels;
using Domain.Enums;
using Domain.Results;
using Infrastructure.API.ApiCache;
using Infrastructure.API.ConvertorInterfaces;
using Infrastructure.API.HttpClientFactory;

namespace Infrastructure.API;

public abstract class CryptocurrenciesApiBase : ICryptocurrenciesApi
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IApiResultCache _apiResultCache;
    
    protected CryptocurrenciesApiBase(string baseUri, IHttpClientFactory httpClientFactory, IApiResultCache apiResultCache)
    {
        _httpClientFactory = httpClientFactory;
        _apiResultCache = apiResultCache;
        _httpClientFactory.SetBaseUri(baseUri);
    }

    public abstract Task<ApiResult<IEnumerable<Coin>>> GetMostPopularCoinsAsync(string? symbolOrName, int? limit);

    public abstract Task<ApiResult<IEnumerable<Ticket>>> GetCoinTicketsAsync(string id);

    public abstract Task<ApiResult<IEnumerable<CoinPriceHistory>>> GetTradesHistoryAsync(string id,
        TradesHistoryIntervals interval);

    public abstract Task<ApiResult<Exchange>> GetFullExchangeInfoAsync(string id);

    protected async Task<ApiResult<T>> GetAndCacheResultAsync<T>(string uri,
        Dictionary<string, object?>? parameters = null,
        IJsonConvertor<T>? jsonConvertor = null)
        where T : class
    {
        var key = new StringBuilder();
        key.Append(uri);
        key.Append(string.Join(",", parameters?.Select(p => p.Value) ?? Array.Empty<string>()));
        key.Append(jsonConvertor?.GetType().Name ?? "");
        
        return await _apiResultCache.Cache(
            key.ToString(), 
            () => GetResultAsObjectAsync(uri, parameters, jsonConvertor));
    }
    
    protected async Task<ApiResult<T>> GetResultAsObjectAsync<T>(string uri, Dictionary<string, object?>? parameters = null,
        IJsonConvertor<T>? jsonConvertor = null) where T : class
    {
        var responseMessage = await GetAsync(uri, parameters);

        if (responseMessage.IsSuccessStatusCode == false)
            return ApiResult.Exception<T>(responseMessage);

        if (jsonConvertor == null)
        {
            var content = await responseMessage.Content.ReadFromJsonAsync<T>();
                
            return content == null 
                ? ApiResult.Exception<T>(new JsonException(
                    $"ReadFromJsonAsync<{typeof(T).Name}> returned null when json is {await responseMessage.Content.ReadAsStringAsync()}")) 
                : ApiResult.Content(content);
        }

        var json = await responseMessage.Content.ReadAsStringAsync();
        return ApiResult.Content(jsonConvertor.Convert(json));
    }
    
    private async Task<HttpResponseMessage> GetAsync(string uri, IReadOnlyDictionary<string, object?>? parameters = null)
    {
        using var httpClient = _httpClientFactory.Create();

        if (parameters == null)
            return await httpClient.GetAsync(uri);
            
        var parametersAsString = ParseParameters(ref uri, parameters);
        return await httpClient.GetAsync(uri + parametersAsString);
    }

    private static string ParseParameters(ref string uri, IReadOnlyDictionary<string, object?> parameters)
    {
        var queriesInUri = Regex.Matches(uri, @"{(.*)}", RegexOptions.IgnoreCase)
            .Select(m => m.Value.Trim('{', '}'));

        foreach (var key in queriesInUri)
            if (parameters.TryGetValue(key, out var value))
                uri = uri.Replace("{" + key + "}", value?.ToString()
                                                   ?? throw new ArgumentNullException($"{key} is haven't value"));

        var parametersAsString = "?" + string.Join("&", parameters
            .Where(p => p.Value != null)
            .Select(p => p.Key + "=" + p.Value));
        return parametersAsString;
    }
}