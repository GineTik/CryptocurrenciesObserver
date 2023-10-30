using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.APIModels;
using Domain.Enums;
using Domain.Results;
using MediatR;

namespace Application.CQRS;

public record GetCoinPriceHistoryResult(IEnumerable<CoinPriceHistory> Content, Exception? Exception = null)
    : ApiResult<IEnumerable<CoinPriceHistory>>(Content, Exception);

public record GetCoinPriceHistoryRequest(string Id, TradesHistoryIntervals Intervals) : IRequest<GetCoinPriceHistoryResult>;

public class GetCoinPriceHistoryHandler : IRequestHandler<GetCoinPriceHistoryRequest, GetCoinPriceHistoryResult>
{
    private readonly ICryptocurrenciesApi _api;

    public GetCoinPriceHistoryHandler(ICryptocurrenciesApi api)
    {
        _api = api;
    }

    public async Task<GetCoinPriceHistoryResult> Handle(GetCoinPriceHistoryRequest request, CancellationToken cancellationToken)
    {
        var result = await _api.GetTradesHistoryAsync(request.Id, request.Intervals);
        return new GetCoinPriceHistoryResult(result.Content, result.Exception);
    }
}