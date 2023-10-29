using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Controls;
using Application.Queries;
using Domain.APIModels;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using UI.UserControls;
using UI.ViewModels.Base;
using UI.Views;

namespace UI.ViewModels;

public class MainViewModel : ViewModelBase
{
    private readonly IServiceProvider _serviceProvider;
    private Page _activePage;
    private readonly HomePage _homePage;
    
    private IEnumerable<Ticket> _tickets;
    private IMediator _mediator;

    public MainViewModel(IServiceProvider serviceProvider, IMediator mediator)
    {
        _serviceProvider = serviceProvider;
        _mediator = mediator;
        _activePage = _homePage = _serviceProvider.GetRequiredService<HomePage>();

        SwitchActivePageCommand = new ViewModelCommand(SwitchPage);
        _homePage.HomeViewModel.OnCoinSelected += LoadTickets;
    }

    public Page ActivePage
    {
        get => _activePage;
        private set => SetField(ref _activePage, value);
    }
        
    public HomeViewModel HomeViewModel => _homePage.HomeViewModel;

    public IEnumerable<Ticket> Tickets
    {
        get => _tickets;
        set => SetField(ref _tickets, value);
    }

    public ViewModelCommand SwitchActivePageCommand { get; }

    private void SwitchPage(object? o)
    {
        var button = (StyledButton)o!;
        var pageType = (Type)button.Data;
        var page = (Page)_serviceProvider.GetRequiredService(pageType);
        ActivePage = page;
    }

    private void LoadTickets(Coin coin)
    {
        Task.Run(async () =>
        {
            var result = await _mediator.Send(new GetCoinTicketsRequest(coin.Id));

            if (result.Successfully)
                Tickets = result.Content;
            else
                Console.WriteLine(result.Exception!.ToString());
        });
    }
}