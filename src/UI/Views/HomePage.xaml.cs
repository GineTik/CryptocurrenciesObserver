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
}