using Application.Interfaces;
using Infrastructure.API;
using Infrastructure.API.ApiCache;
using Infrastructure.API.CoinCap;
using Infrastructure.API.ConvertorInterfaces;
using Infrastructure.API.HttpClientFactory;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<ICryptocurrenciesApi, CoinCapApi>();
        services.AddTransient<IHttpClientFactory, HttpClientFactory>();
        services.AddTransient<ICoinsJsonConvertor, CoinsJsonConvertor>();
        services.AddTransient<ITicketsJsonConvertor, TicketsJsonConvertor>();
        services.AddTransient<ICoinPriceHistoryJsonConverter, CoinPriceHistoryJsonConvertor>();
        services.AddTransient<IExchangeJsonConvertor, ExchangeJsonConverter>();
        services.AddTransient<IHistoryRepository, MemoryHistoryRepository>();
        services.AddTransient<IApiResultCache, ApiResultCache>();
        return services;
    }
}