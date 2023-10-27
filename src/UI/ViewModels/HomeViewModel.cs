using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using Application.Queries;
using Domain.APIModels;
using MediatR;
using UI.ViewModels.Base;

namespace UI.ViewModels;

public class HomeViewModel : ViewModelBase
{
    private IEnumerable<Coin> _coins;

    private readonly IMediator _mediator;

    public HomeViewModel(IMediator mediator)
    {
        _mediator = mediator;
        _coins = Array.Empty<Coin>();
        LoadCoins();
    }
    
    public IEnumerable<Coin> Coins
    {
        get => _coins;
        private set => SetField(ref _coins, value);
    }
    
    private async Task LoadCoins()
    {
        try
        {
            var result = await _mediator.Send(new GetMostPopularCoinsQuery());
            
            if (result.Successfully)
            {
                Coins = result.Content;
            }
            else
            {
                Console.WriteLine(result.Exception!.Message);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}