using System.Collections.Generic;

namespace Domain.APIModels;

public class Ticket
{
    public string BaseSymbol { get; set; } = null!;
    public string TargetSymbol { get; set; } = null!;
    public string BaseCoinId { get; set; } = null!;
    public string TargetCoinId { get; set; } = null!;
    public decimal Price { get; set; }
    public string ExchangeId { get; set; } = null!;
    public string ExchangeName { get; set; } = null!;
}