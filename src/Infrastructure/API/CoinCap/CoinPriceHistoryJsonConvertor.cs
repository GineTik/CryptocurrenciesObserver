using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json;
using Domain.APIModels;
using Infrastructure.API.ConvertorInterfaces;

namespace Infrastructure.API.CoinCap;

public class CoinPriceHistoryJsonConvertor : ICoinPriceHistoryJsonConverter
{
    public IEnumerable<CoinPriceHistory> Convert(string json)
    {
        using var document = JsonDocument.Parse(json);
        var root = document.RootElement;

        foreach (var element in root.GetProperty("data").EnumerateArray())
        {
            yield return new CoinPriceHistory
            {
                Price = decimal.Parse(element.GetProperty("priceUsd").GetString()!, CultureInfo.InvariantCulture),
                Time = DateTime.UtcNow.Subtract(new TimeSpan(element.GetProperty("time").GetInt64()))
            };
        }
    }
}