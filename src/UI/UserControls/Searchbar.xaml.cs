using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace UI.UserControls;

public partial class Searchbar
{
    public Searchbar()
    {
        InitializeComponent();
    }
    
    public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register(nameof(Text), typeof(string), typeof(Searchbar), new PropertyMetadata(null));

    public static readonly DependencyProperty TextChangedProperty =
        DependencyProperty.Register(nameof(TextChanged), typeof(Action<string>), typeof(Searchbar), new PropertyMetadata(null));

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
    
    public Action<string>? TextChanged
    {
        get => (Action<string>?)GetValue(TextChangedProperty);
        set => SetValue(TextChangedProperty, value);
    }

    private void SearchBoxGotFocus(object sender, RoutedEventArgs e)
    {
        Placeholder.Visibility = Visibility.Collapsed;
        Placeholder.Width = 0;
    }

    private void SearchBoxLostFocus(object sender, RoutedEventArgs e)
    {
        if (!string.IsNullOrEmpty(SearchBox.Text)) return;
        Placeholder.Visibility = Visibility.Visible;
        Placeholder.Width = 200;
    }
    
    private void BorderOnMouseDown(object sender, MouseButtonEventArgs e)
    {
        SearchBox.Focus();
    }

    private void SearchBoxOnTextChanged(object sender, TextChangedEventArgs e)
    {
        TextChanged?.Invoke(SearchBox.Text);
    }
}