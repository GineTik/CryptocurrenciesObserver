using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.APIModels;
using Domain.Results;
using MediatR;

namespace Application.CQRS;

public record GetMostPopularCoinsResult(IEnumerable<Coin> Content, Exception? Exception) 
    : RequestResult<IEnumerable<Coin>>(Content, Exception);

public record GetMostPopularCoinsRequest(string? SymbolOrName = null) : IRequest<GetMostPopularCoinsResult>;

public class GetMostPopularCoinsHandler : IRequestHandler<GetMostPopularCoinsRequest, GetMostPopularCoinsResult>
{
    private readonly ICryptocurrenciesApi _api;

    public GetMostPopularCoinsHandler(ICryptocurrenciesApi api)
    {
        _api = api;
    }

    public async Task<GetMostPopularCoinsResult> Handle(GetMostPopularCoinsRequest request, CancellationToken cancellationToken)
    {
        var apiResult = await _api.GetMostPopularCoinsAsync(symbolOrName: request.SymbolOrName, limit: 20);
        return new GetMostPopularCoinsResult(apiResult.Content, apiResult.Exception);
    }
}