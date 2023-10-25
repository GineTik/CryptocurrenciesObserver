using System;
using System.Globalization;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using FontAwesome.WPF;

namespace UI.UserControls;

public partial class StyledButton
{
    public StyledButton()
    {
        InitializeComponent();
        DataContext = this;
    }
      
    public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register(nameof(Text), typeof(string), typeof(StyledButton), new PropertyMetadata("Text"));
    
    public static readonly DependencyProperty IconProperty =
        DependencyProperty.Register(nameof(Icon), typeof(FontAwesomeIcon), typeof(StyledButton), new PropertyMetadata(FontAwesomeIcon.FontAwesome));
    
    public static readonly DependencyProperty IsActiveProperty =
        DependencyProperty.Register(nameof(IsActive), typeof(bool), typeof(StyledButton), new PropertyMetadata(false));
 
    public static readonly DependencyProperty ClickProperty =
        DependencyProperty.Register(nameof(Click), typeof(Action<StyledButton>), typeof(StyledButton), new PropertyMetadata(null));

    public new static readonly DependencyProperty ForegroundProperty =
        DependencyProperty.Register(nameof(Foreground), typeof(Brush), typeof(StyledButton), new PropertyMetadata(null, OnForegroundPropertyChanged));
    
    public static readonly DependencyProperty DataProperty =
        DependencyProperty.Register(nameof(Data), typeof(object), typeof(StyledButton), new PropertyMetadata(null));
    
    public static readonly DependencyProperty CommandProperty =
        DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(StyledButton), new PropertyMetadata(null));

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
    
    public FontAwesomeIcon Icon
    {
        get => (FontAwesomeIcon)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public bool IsActive
    {
        get => (bool)GetValue(IsActiveProperty);
        set => SetValue(IsActiveProperty, value);
    }
    
    public Action<StyledButton>? Click
    {
        get => (Action<StyledButton>)GetValue(ClickProperty);
        set => SetValue(ClickProperty, value);
    }
    
    public new Brush? Foreground
    {
        get => (Brush)GetValue(ForegroundProperty);
        set => SetValue(ForegroundProperty, value);
    }
    
    public object Data
    {
        get => GetValue(DataProperty);
        set => SetValue(DataProperty, value);
    }
    
    public ICommand? Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    private static void OnForegroundPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
    {
        if (dependencyObject is StyledButton { Foreground: not null } button)
        {
            button.Button.Foreground = button.Foreground;
        }
    }
    
    private void ButtonClick(object sender, RoutedEventArgs e)
    {
        Click?.Invoke(this);
        if (Command?.CanExecute(this) == true)
            Command?.Execute(this);
    }
}