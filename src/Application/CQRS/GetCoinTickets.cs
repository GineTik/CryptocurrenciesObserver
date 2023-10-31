using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.APIModels;
using Domain.Results;
using MediatR;

namespace Application.CQRS;

public record GetCoinTicketsRequest(string Id) : IRequest<RequestResult<IEnumerable<Ticket>>>;

public class GetCoinTicketsHandler : IRequestHandler<GetCoinTicketsRequest, RequestResult<IEnumerable<Ticket>>>
{
    private readonly ICryptocurrenciesApi _api;

    public GetCoinTicketsHandler(ICryptocurrenciesApi api)
    {
        _api = api;
    }
    
    public async Task<RequestResult<IEnumerable<Ticket>>> Handle(GetCoinTicketsRequest request, CancellationToken cancellationToken)
    {
        return await _api.GetCoinTicketsAsync(request.Id);
    }
}