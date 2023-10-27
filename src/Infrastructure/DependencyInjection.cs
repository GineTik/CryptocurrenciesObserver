using Application.Interfaces;
using Infrastructure.API;
using Infrastructure.API.ConvertorInterfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<ICryptocurrenciesApi, CoinCupApi>();
        services.AddTransient<IHttpClientFactory, HttpClientFactory>();
        services.AddTransient<ICoinsJsonConvertor, CoinsJsonConvertor>();
        return services;
    }
}