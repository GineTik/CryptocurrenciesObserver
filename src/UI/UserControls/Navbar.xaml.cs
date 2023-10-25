using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace UI.UserControls;

public partial class Navbar
{
    private StyledButton _currentStyledButton;
    
    public Navbar()
    {
        InitializeComponent();

        if (NavbarPanel.Children.Count == 0)
            throw new InvalidOperationException("Navbar items is 0");

        if (NavbarPanel.Children[0] is not StyledButton styledButton)
            throw new InvalidDataException("Navbar should contains only NavbarButton elements");

        _currentStyledButton = styledButton;
    }

    public static readonly DependencyProperty ItemSelectedProperty =
        DependencyProperty.Register(nameof(ItemSelected), typeof(Action<StyledButton>), typeof(Navbar), new PropertyMetadata(null));

    public static readonly DependencyProperty CommandProperty =
        DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(Navbar), new PropertyMetadata(null));
    
    public Action<StyledButton> ItemSelected
    {
        get => (Action<StyledButton>)GetValue(ItemSelectedProperty);
        set => SetValue(ItemSelectedProperty, value);
    }
    
    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    private void NavbarButtonClick(StyledButton sender)
    {
        if (sender.IsActive == true)
            return;
        
        sender.IsActive = true;
        _currentStyledButton.IsActive = false;
        _currentStyledButton = sender;

        ItemSelected?.Invoke(sender);
    }
}