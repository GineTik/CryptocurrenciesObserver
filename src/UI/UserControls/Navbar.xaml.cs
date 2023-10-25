using System;
using System.IO;
using System.Windows;

namespace UI.UserControls;

public partial class Navbar
{
    private StyledButton _currentStyledButton;
    
    public Navbar()
    {
        InitializeComponent();

        if (NavbarPanel.Children.Count == 0)
            throw new InvalidOperationException("Navbar items is 0");

        _currentStyledButton =  NavbarPanel.Children[0] as StyledButton 
                          ?? throw new InvalidDataException("Navbar should contains only NavbarButton elements");
    }

    private void NavbarButtonClick(StyledButton sender)
    {
        sender.IsActive = true;
        _currentStyledButton.IsActive = false;
        _currentStyledButton = sender;
    }
}