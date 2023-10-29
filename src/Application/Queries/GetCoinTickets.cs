using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.APIModels;
using Domain.Results;
using MediatR;

namespace Application.Queries;

public record GetCoinTicketsResult(IEnumerable<Ticket> Content, Exception? Exception) 
    : QueryResult<IEnumerable<Ticket>>(Content, Exception);

public record GetCoinTicketsRequest(string Id) : IRequest<GetCoinTicketsResult>;

public class GetCoinTicketsHandler : IRequestHandler<GetCoinTicketsRequest, GetCoinTicketsResult>
{
    private readonly ICryptocurrenciesApi _api;

    public GetCoinTicketsHandler(ICryptocurrenciesApi api)
    {
        _api = api;
    }
    
    public async Task<GetCoinTicketsResult> Handle(GetCoinTicketsRequest request, CancellationToken cancellationToken)
    {
        var result = await _api.GetCoinTicketsAsync(request.Id);
        return new GetCoinTicketsResult(result.Content, result.Exception);
    }
}