using System.Threading.Tasks;
using Domain.Models;

namespace Application.Interfaces;

public interface IHistoryRepository
{
    Task<HistoryItem?> GetLastPushedHistoryAsync();
    Task PushToHistoryAsync(HistoryItem item);
}