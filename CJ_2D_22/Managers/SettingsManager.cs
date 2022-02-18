using System;
using System.Collections.Generic;

namespace CJ_2D_22.Managers;
public static class SettingsManager
{
    public static List<string> Themes = new();
    public static string ChosenTheme = string.Empty;

    public static void Initialize()
    {
        LoadThemeSettings();
    }

    private static void LoadThemeSettings()
    {
        try
        {
            if (Themes == null || Themes.Count == 0)
            {
                Themes = FileManager.GetThemes() ?? throw new Exception("Error getting ");
                ChosenTheme = FileManager.GetChosenTheme();
            }
        }
        catch (Exception e)
        {
            Themes = new List<string>();
            LogManager.AddLog("Error loading themes: " + e.Message, "Settings Manager");
        }
    }

    public static void ChangeChosenTheme(string theme)
    {
        FileManager.SetChosenTheme(theme);
        ChosenTheme = theme;
    }
}

