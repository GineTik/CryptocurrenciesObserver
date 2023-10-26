using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace UI.UserControls;

public partial class StyledTable
{
    public StyledTable()
    {
        InitializeComponent();
        DataContext = this;
    }
    
    public static readonly DependencyProperty ItemsProperty =
        DependencyProperty.Register(
            nameof(Items), 
            typeof(ObservableCollection<object>), 
            typeof(StyledTable));

    public static readonly DependencyProperty ItemTemplateProperty =
        DependencyProperty.Register(
            nameof(ItemTemplate),
            typeof(DataTemplate), 
            typeof(StyledTable));
    
    public IEnumerable Items
    {
        get => (ObservableCollection<object>)GetValue(ItemsProperty);
        set => SetValue(ItemsProperty, value);
    }
    
    public DataTemplate ItemTemplate
    {
        get => (DataTemplate) GetValue(ItemTemplateProperty);
        set => SetValue(ItemTemplateProperty, value);
    }
}