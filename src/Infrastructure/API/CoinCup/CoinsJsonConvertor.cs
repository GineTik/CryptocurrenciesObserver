﻿using System.Collections.Generic;
using System.Globalization;
using System.Text.Json;
using Domain.APIModels;
using Infrastructure.API.ConvertorInterfaces;

namespace Infrastructure.API;

public class CoinsJsonConvertor : ICoinsJsonConvertor
{
    public IEnumerable<Coin> Convert(string json)
    { 
        using var document = JsonDocument.Parse(json);
        var root = document.RootElement;

        foreach (var element in root.GetProperty("data").EnumerateArray())
        {
            yield return new Coin
            {
                Id = element.GetProperty("id").GetString()!,
                Symbol = element.GetProperty("symbol").GetString()!,
                Name = element.GetProperty("name").GetString()!,
                CurrentPrice = decimal.Parse(element.GetProperty("priceUsd").GetString()!, CultureInfo.InvariantCulture),
                Rank = int.Parse(element.GetProperty("rank").GetString()!)
            };
        }
    }
}