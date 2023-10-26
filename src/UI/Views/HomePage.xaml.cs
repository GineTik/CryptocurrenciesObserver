using System.Collections;
using System.Collections.Generic;
using System.Windows.Controls;
using Domain.APIModels;
using UI.ViewModels;

namespace UI.Views;

public partial class HomePage : Page
{
    public HomePage(HomeViewModel homeViewModel)
    {
        InitializeComponent();
        DataContext = homeViewModel;
    }

    // public IEnumerable<ExchangeShort> Exhanges => new[]
    // {
    //     new ExchangeShort { Id = "binance", Name = "Binance" },
    //     new ExchangeShort { Id = "test1", Name = "Test1" },
    //     new ExchangeShort { Id = "test2", Name = "Test2" },
    //     new ExchangeShort { Id = "test3", Name = "Test3" }
    // };
}