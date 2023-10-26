using System.Collections.Generic;

namespace Domain.APIModels;

public class Ticket
{
    public string BaseSymbol { get; set; } = null!;
    public string TargetSymbol { get; set; } = null!;
    public string BaseCoinId { get; set; } = null!;
    public string TargetCoinId { get; set; } = null!;
    public IEnumerable<ExchangeShort> Exchanges { get; set; } = null!;
}