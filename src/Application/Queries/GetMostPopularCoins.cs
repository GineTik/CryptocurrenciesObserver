using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.APIModels;
using Domain.Results;
using MediatR;

namespace Application.Queries;

public record GetMostPopularCoinsResult(IEnumerable<Coin> Content, Exception? Exception) 
    : QueryResult<IEnumerable<Coin>>(Content, Exception);

public record GetMostPopularCoinsQuery() : IRequest<GetMostPopularCoinsResult>;

public class GetMostPopularCoinsHandler : IRequestHandler<GetMostPopularCoinsQuery, GetMostPopularCoinsResult>
{
    private readonly ICryptocurrenciesApi _api;

    public GetMostPopularCoinsHandler(ICryptocurrenciesApi api)
    {
        _api = api;
    }

    public async Task<GetMostPopularCoinsResult> Handle(GetMostPopularCoinsQuery request, CancellationToken cancellationToken)
    {
        var apiResult = await _api.GetMostPopularCoinsAsync();
        return new GetMostPopularCoinsResult(apiResult.Content, apiResult.Exception);
    }
}