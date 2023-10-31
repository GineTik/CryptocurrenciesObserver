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

public record GetCoinPriceHistoryRequest(string Id, TradesHistoryIntervals Intervals) : IRequest<RequestResult<IEnumerable<CoinPriceHistory>>>;

public class GetCoinPriceHistoryHandler : IRequestHandler<GetCoinPriceHistoryRequest, RequestResult<IEnumerable<CoinPriceHistory>>>
{
    private readonly ICryptocurrenciesApi _api;

    public GetCoinPriceHistoryHandler(ICryptocurrenciesApi api)
    {
        _api = api;
    }

    public async Task<RequestResult<IEnumerable<CoinPriceHistory>>> Handle(GetCoinPriceHistoryRequest request, CancellationToken cancellationToken)
    {
        return await _api.GetTradesHistoryAsync(request.Id, request.Intervals);
    }
}