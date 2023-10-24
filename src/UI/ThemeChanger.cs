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
        var resources = CreateResourceDictionaryByTheme(theme);
        RemoveAllThemeResources();
        Application.Current.Resources.MergedDictionaries.Add(resources);
    }

    private static void RemoveAllThemeResources()
    {
        var themeResources = new ResourceDictionary[]
        {
            CreateResourceDictionaryByTheme(Themes.Light),
            CreateResourceDictionaryByTheme(Themes.Dark),
        };
        
        foreach (var resource in themeResources)
           Application.Current.Resources.MergedDictionaries.Remove(resource);
    }

    private static ResourceDictionary CreateResourceDictionaryByTheme(Themes theme)
    {
        var uri = new Uri($"Resources/Themes/{theme}.xaml", UriKind.Relative);
        return new ResourceDictionary {Source = uri};
    }
}