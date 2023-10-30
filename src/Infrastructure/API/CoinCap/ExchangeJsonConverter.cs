using System.Text.Json;
using Domain.APIModels;
using Infrastructure.API.ConvertorInterfaces;

namespace Infrastructure.API.CoinCap;

public class ExchangeJsonConverter : IExchangeJsonConvertor
{
    public Exchange Convert(string json)
    {
        using var document = JsonDocument.Parse(json);
        var root = document.RootElement.GetProperty("data");

        return new Exchange
        {
            Id = root.GetProperty("exchangeId").GetString()!,
            Name = root.GetProperty("name").GetString()!,
            Url = root.GetProperty("exchangeUrl").GetString()!,
            Image = null,
        };
    }
}