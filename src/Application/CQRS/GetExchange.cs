using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.APIModels;
using Domain.Results;
using MediatR;

namespace Application.CQRS;

public record GetExchangeRequest(string Id) : IRequest<RequestResult<Exchange>>;

public class GetExchangeHandler : IRequestHandler<GetExchangeRequest, RequestResult<Exchange>>
{
    private readonly ICryptocurrenciesApi _api;

    public GetExchangeHandler(ICryptocurrenciesApi api)
    {
        _api = api;
    }

    public async Task<RequestResult<Exchange>> Handle(GetExchangeRequest request, CancellationToken cancellationToken)
    {
        return await _api.GetFullExchangeInfoAsync(request.Id);
    }
}