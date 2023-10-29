using Application.Interfaces;
using Domain.APIModels;
using Infrastructure.API;
using Infrastructure.API.CoinCap;
using Infrastructure.API.ConvertorInterfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<ICryptocurrenciesApi, CoinCapApiBase>();
        services.AddTransient<IHttpClientFactory, HttpClientFactory>();
        services.AddTransient<ICoinsJsonConvertor, CoinsJsonConvertor>();
        services.AddTransient<ITicketsJsonConvertor, TicketsJsonConvertor>();
        return services;
    }
}