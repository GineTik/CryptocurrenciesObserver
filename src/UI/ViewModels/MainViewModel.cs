using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Controls;
using Application.CQRS;
using Domain.APIModels;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using UI.ViewModels.Base;
using UI.Views;

namespace UI.ViewModels;

public class MainViewModel : ViewModelBase
{
    private readonly IServiceProvider _serviceProvider;
    private Page _activePage;
    private readonly HomePage _homePage;
    
    private IEnumerable<Ticket>? _tickets;
    private readonly IMediator _mediator;
    private readonly CoinInformationViewModel _coinInformationViewModel;

    public MainViewModel(IServiceProvider serviceProvider, IMediator mediator, CoinInformationViewModel coinInformationViewModel)
    {
        _serviceProvider = serviceProvider;
        _mediator = mediator;
        _coinInformationViewModel = coinInformationViewModel;
        _activePage = _homePage = _serviceProvider.GetRequiredService<HomePage>();

        SwitchActivePageCommand = new ViewModelCommand(SwitchPage);
        _homePage.HomeViewModel.OnCoinSelected += LoadTickets;
    }

    public Page ActivePage
    {
        get => _activePage;
        private set => SetField(ref _activePage, value);
    }

    public IEnumerable<Ticket>? Tickets
    {
        get => _tickets;
        private set => SetField(ref _tickets, value);
    }
    
    public HomeViewModel HomeViewModel => _homePage.HomeViewModel;

    public CoinInformationViewModel CoinInformationViewModel => _coinInformationViewModel;

    public ViewModelCommand SwitchActivePageCommand { get; }

    private void SwitchPage(object? o)
    {
        ArgumentNullException.ThrowIfNull(o);
        
        var pageType = (Type)o;
        var page = (Page)_serviceProvider.GetRequiredService(pageType);
        ActivePage = page;
    }

    private void LoadTickets(Coin coin)
    {
        Task.Run(async () =>
        {
            var result = await _mediator.Send(new GetCoinTicketsRequest(coin.Id));

            if (result.Successfully)
            {
                Tickets = result.Content;
                await _mediator.Publish(new PushToHistoryNotification(new HistoryItem
                {
                    Coin = coin,
                    Tickets = Tickets
                }));
            }
            else
            {
                Console.WriteLine(result.Exception!.ToString());
            }
        });
    }
}