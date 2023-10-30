using UI.ViewModels;

namespace UI.Views;

public partial class CoinInformationPage
{
    public CoinInformationPage(CoinInformationViewModel coinInformationViewModel)
    {
        InitializeComponent();
        DataContext = coinInformationViewModel;
    }
}