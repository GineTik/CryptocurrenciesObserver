using System;
using System.Windows.Controls;
using Domain.APIModels;
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

    public MainViewModel(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _activePage = _homePage = _serviceProvider.GetRequiredService<HomePage>();

        SwitchActivePageCommand = new ViewModelCommand(SwitchPage);
    }

    public Page ActivePage
    {
        get => _activePage;
        private set => SetField(ref _activePage, value);
    }
        
    public HomeViewModel HomeViewModel => _homePage.HomeViewModel;

    public ViewModelCommand SwitchActivePageCommand { get; }

    private void SwitchPage(object? o)
    {
        var button = (StyledButton)o!;
        var pageType = (Type)button.Data;
        var page = (Page)_serviceProvider.GetRequiredService(pageType);
        ActivePage = page;
    }
}