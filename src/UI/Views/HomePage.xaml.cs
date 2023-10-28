using UI.ViewModels;

namespace UI.Views;

public partial class HomePage
{
    public HomePage(HomeViewModel homeViewModel)
    {
        InitializeComponent();
        DataContext = homeViewModel;
    }
}