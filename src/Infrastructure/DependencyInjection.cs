using Application.Interfaces;
using Infrastructure.API;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<ICryptocurrenciesApi, CoinGeckoApi>();
        return services;
    }
}