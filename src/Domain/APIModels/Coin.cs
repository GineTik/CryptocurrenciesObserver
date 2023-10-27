using System;

namespace Domain.APIModels;

public class Coin
{
    public string Id { get; set; } = null!;
    public string Symbol { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Image { get; set; } = null!;
    public decimal CurrentPrice { get; set; }
    public int Rank { get; set; }
}