using System;
using System.Windows;
using System.Windows.Controls;

namespace UI.UserControls;

public partial class ThemeSwitcher
{
    private readonly ControlTemplate _lightButton;
    private readonly ControlTemplate _darkButton;
    
    public ThemeSwitcher()
    {
        InitializeComponent();
        
        _lightButton = FindResource("ToLightButton") as ControlTemplate ?? throw new InvalidOperationException();
        _darkButton = FindResource("ToDarkButton") as ControlTemplate ?? throw new InvalidOperationException();
    }

    private void SetLightTheme(object o, RoutedEventArgs routedEventArgs)
    {
        ThemeChanger.Change(Themes.Light);
        ThemeButton.Template = _darkButton;
    }

    private void SetDarkTheme(object o, RoutedEventArgs routedEventArgs)
    {
        ThemeChanger.Change(Themes.Dark);
        ThemeButton.Template = _lightButton;
    }
}