using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Models;
using MediatR;

namespace Application.CQRS;


public record PushToHistoryNotification(HistoryItem Item) : INotification;

public class PushToHistoryCoinHandler : INotificationHandler<PushToHistoryNotification>
{
    private readonly IHistoryRepository _historyRepository;

    public PushToHistoryCoinHandler(IHistoryRepository historyRepository)
    {
        _historyRepository = historyRepository;
    }

    public async Task Handle(PushToHistoryNotification notification, CancellationToken cancellationToken)
    {
        await _historyRepository.PushToHistoryAsync(notification.Item);
    }
}