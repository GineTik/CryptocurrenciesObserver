using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Domain.APIModels;
using UI.ViewModels.Base;

namespace UI.ViewModels;

public class HomeViewModel : ViewModelBase
{
    public IEnumerable<CoinMarket> Coins
    {
        get;
        set;
    }
}