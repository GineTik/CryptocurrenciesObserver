using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace UI.UserControls;

public partial class Navbar
{
    private Button _currentStyledButton;
    
    public Navbar()
    {
        InitializeComponent();

        if (NavbarPanel.Children.Count == 0)
            throw new InvalidOperationException("Navbar items is 0");

        if (NavbarPanel.Children[0] is not Button styledButton)
            throw new InvalidDataException("Navbar should contains only NavbarButton elements");

        _currentStyledButton = styledButton;
    }
    
    public static readonly DependencyProperty CommandProperty =
        DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(Navbar), new PropertyMetadata(null));
    
    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    private void NavbarButtonClick(object o, RoutedEventArgs routedEventArgs)
    {
        var button = o as Button ?? throw new InvalidCastException($"{o.GetType()} is not Button");
        var grayButtonStyle = System.Windows.Application.Current.FindResource("GrayButton") as Style;
        var primaryButtonStyle = System.Windows.Application.Current.FindResource("PrimaryButton") as Style;

        if (button.Style == primaryButtonStyle)
            return;
        
        button.Style = primaryButtonStyle;
        _currentStyledButton.Style = grayButtonStyle;
        _currentStyledButton = button;
    }
}