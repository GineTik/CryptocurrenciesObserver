using System.Collections.Generic;
using Domain.APIModels;

namespace Domain.Models;

public class HistoryItem
{
    public Coin Coin { get; init; } = null!;
    public IEnumerable<Ticket>? Tickets { get; init; } = null!;
}