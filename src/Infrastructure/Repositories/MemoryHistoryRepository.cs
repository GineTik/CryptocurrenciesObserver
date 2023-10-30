using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Models;

namespace Infrastructure.Repositories;

public class MemoryHistoryRepository : IHistoryRepository
{
    private static readonly List<HistoryItem> History = new();
    
    public Task<HistoryItem?> GetLastPushedHistoryAsync()
    {
        return Task.FromResult(History.LastOrDefault());
    }

    public async Task PushToHistoryAsync(HistoryItem item)
    {
        var lastItem = await GetLastPushedHistoryAsync();
        if (lastItem?.Coin != item.Coin)
            History.Add(item);
    }
}