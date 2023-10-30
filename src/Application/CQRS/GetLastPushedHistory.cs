using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Models;
using Domain.Results;
using MediatR;

namespace Application.CQRS;

public record GetLastPushedHistoryRequest : IRequest<RequestResult<HistoryItem>>;

internal class GetLastPushedHistoryHandler : IRequestHandler<GetLastPushedHistoryRequest, RequestResult<HistoryItem>>
{
    private readonly IHistoryRepository _historyRepository;

    public GetLastPushedHistoryHandler(IHistoryRepository historyRepository)
    {
        _historyRepository = historyRepository;
    }

    public async Task<RequestResult<HistoryItem>> Handle(GetLastPushedHistoryRequest request, CancellationToken cancellationToken)
    {
        var history = await _historyRepository.GetLastPushedHistoryAsync();
        return history == null 
            ? new RequestResult<HistoryItem>(new InvalidOperationException("History is empty"))
            : new  RequestResult<HistoryItem>(history);
    }
}