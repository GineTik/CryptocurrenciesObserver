using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json;
using Domain.APIModels;
using Infrastructure.API.ConvertorInterfaces;

namespace Infrastructure.API.CoinCap;

public class TicketsJsonConvertor : ITicketsJsonConvertor
{
    public IEnumerable<Ticket> Convert(string json)
    {
        using var document = JsonDocument.Parse(json);
        var root = document.RootElement;

        foreach (var element in root.GetProperty("data").EnumerateArray())
        {
            var isDecimal = decimal.TryParse(element.GetProperty("priceUsd").GetString()!, 
                CultureInfo.InvariantCulture,
                out var price);
            
            // TODO: solve the problem with the inability to compare prices
            if (isDecimal == false)
                Console.WriteLine($"Element: {element.ToString()}, Price: {element.GetProperty("priceUsd").GetString()}");
            
            yield return new Ticket
            {
                BaseCoinId = element.GetProperty("baseId").GetString()!,
                TargetCoinId = element.GetProperty("quoteId").GetString()!,
                BaseSymbol = element.GetProperty("baseSymbol").GetString()!,
                TargetSymbol = element.GetProperty("quoteSymbol").GetString()!,
                Price = isDecimal ? price : -1, // sometimes throws an error, then return -1
                ExchangeId = element.GetProperty("exchangeId").GetString()!,
                ExchangeName = element.GetProperty("exchangeId").GetString()!
            };
        }
    }
}