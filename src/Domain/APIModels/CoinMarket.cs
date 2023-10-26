using System;

namespace Domain.APIModels;

public class CoinMarket
{
    public string Id { get; set; } = null!;
    public string Symbol { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Image { get; set; } = null!;
    public decimal CurrentPrice { get; set; }
    public long MarketCup { get; set; }
    public int MarketCupRank { get; set; }
    public DateTime LastUpdated { get; set; }
    public decimal High24H { get; set; }
    public decimal Low24H { get; set; }
}