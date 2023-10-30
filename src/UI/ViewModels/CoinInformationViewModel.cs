using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Application.CQRS;
using Domain.APIModels;
using Domain.Enums;
using MediatR;
using UI.ViewModels.Base;

namespace UI.ViewModels;

public class CoinInformationViewModel : ViewModelBase
{
    private readonly IMediator _mediator;
    private IEnumerable<Ticket>? _tickets;
    private IEnumerable<CoinPriceHistory>? _tradesHistory;

    public CoinInformationViewModel(IMediator mediator)
    {
        _mediator = mediator;
        OpenExchangeLinkCommand = new ViewModelCommand(OpenExchangeLink);
        LoadTickets();
    }

    public IEnumerable<Ticket>? Tickets
    {
        get => _tickets;
        private set => SetField(ref _tickets, value);
    }

    public IEnumerable<CoinPriceHistory>? TradesHistory
    {
        get => _tradesHistory;
        private set => SetField(ref _tradesHistory, value);
    }

    public ViewModelCommand OpenExchangeLinkCommand { get; }

    private void OpenExchangeLink(object? parameter)
    {
        Task.Run(async () =>
        {
            ArgumentNullException.ThrowIfNull(parameter);
            var exchangeId = parameter.ToString();

            var response = await _mediator.Send(new GetExchangeRequest(exchangeId!));

            if (response.Successfully == false)
            {
                Console.Write(response.Exception!.ToString());
                return;
            }

            LinkHelper.OpenLink(response.Content.Url);
        });
    }

    private void LoadTickets()
    {
        Task.Run(async () =>
        {
            var historyResponse = await _mediator.Send(new GetLastPushedHistoryRequest());
            if (historyResponse.Successfully == false)
            {
                Console.Write(historyResponse.Exception!.ToString());
                return;
            }
                
            var coin = historyResponse.Content.Coin;

            var ticketsResponse = await _mediator.Send(new GetCoinTicketsRequest(coin.Id));
            Tickets = ticketsResponse.Content;
                
            var tradesResponse = await _mediator.Send(new GetCoinPriceHistoryRequest(coin.Id, TradesHistoryIntervals.H2));
            TradesHistory = tradesResponse.Content;
 
            if (ticketsResponse.Successfully == false || tradesResponse.Successfully == false)
                Console.Write(ticketsResponse.Exception?.ToString() 
                              ?? tradesResponse.Exception!.ToString());
        });
    }
}