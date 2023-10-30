using System;

namespace Domain.APIModels;

public class CoinPriceHistory
{
    public decimal Price { get; set; }
    public DateTime Time { get; set; }
}