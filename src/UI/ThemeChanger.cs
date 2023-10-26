using System;
using System.Windows;

namespace UI;

public enum Themes
{
    Light,
    Dark,
}

public static class ThemeChanger
{
    public static void Change(Themes theme)
    {
        var resources = CreateResourceDictionary(theme);
        RemoveAllThemeResources();
        App.Current.Resources.MergedDictionaries.Add(resources);
    }

    private static void RemoveAllThemeResources()
    {
        var themeResources = new ResourceDictionary[]
        {
            CreateResourceDictionary(Themes.Light),
            CreateResourceDictionary(Themes.Dark),
        };
        
        foreach (var resource in themeResources)
           App.Current.Resources.MergedDictionaries.Remove(resource);
    }

    private static ResourceDictionary CreateResourceDictionary(Themes theme)
    {
        var uri = new Uri($"Resources/Themes/{theme}.xaml", UriKind.Relative);
        return new ResourceDictionary {Source = uri};
    }
}