using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Threading;
using Application.Queries;
using Domain.APIModels;
using MediatR;
using Microsoft.Extensions.Configuration;
using UI.ViewModels.Base;

namespace UI.ViewModels;

public class HomeViewModel : ViewModelBase
{
    private ObservableCollection<Coin> _coins;
    private string _searchText;
    private readonly DispatcherTimer _searchTimer;

    private readonly IMediator _mediator;

    public HomeViewModel(IMediator mediator, IConfiguration configuration)
    {
        _mediator = mediator;
        _coins = new ObservableCollection<Coin>();
        _searchText = "";
        _searchTimer = new DispatcherTimer();
        _searchTimer.Tick += SearchTimerTick;
        var interval = Convert.ToInt32(configuration["SearchIntervalInMilliseconds"]);
        _searchTimer.Interval = TimeSpan.FromMilliseconds(interval);
        
        SearchCommand = new ViewModelCommand(SearchCoins);
        SearchCommand.Execute(null);
    }

    public ViewModelCommand SearchCommand { get; }
    
    public ObservableCollection<Coin> Coins
    {
        get => _coins;
        private set => SetField(ref _coins, value);
    }

    public string SearchText
    {
        get => _searchText;
        set => SetField(ref _searchText, value);
    }
    
    private void SearchCoins(object? parameter)
    {
        _searchTimer.Stop();
        _searchTimer.Start();
    }

    private void SearchTimerTick(object? sender, EventArgs e)
    {
        _searchTimer.Stop();
        Search();
    }

    private void Search()
    {
        Task.Run(async () =>
        {
            var result = await _mediator.Send(new GetMostPopularCoinsQuery(SearchText));

            if (result.Successfully)
                Coins = new ObservableCollection<Coin>(result.Content);
            else
                Console.WriteLine(result.Exception!.Message);
        });
    }
}