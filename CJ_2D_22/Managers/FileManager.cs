using System.Collections.Generic;
using System.Text.Json;
using System.IO;
using System;

namespace CJ_2D_22.Managers;

public static class FileManager
{
    public const string SettingsFolder = @"Settings\";
    public const string ThemesFile = $@"{SettingsFolder}Themes.json";
    public const string SettingsFile = $@"{SettingsFolder}Settings.json";

    public static List<string>? GetThemes()
    {        
        var result = DeserializeObject<List<string>>(ThemesFile);
        return result;
    }

    public static string GetChosenTheme()
    {
        var settings = GetSettings();
        return settings["ChosenTheme"].ToString();
    }

    public static void SaveThemes(List<string> themes)
    {
        SerializeObject(themes, ThemesFile);
    }
    public static void SetChosenTheme(string themeName)
    {
        var settings = GetSettings();
        
        if (settings.ContainsKey("ChosenTheme"))
            settings["ChosenTheme"] = themeName;
        else
            settings.Add("ChosenTheme", themeName);

        SerializeObject(settings, SettingsFile);
    }

    public static Dictionary<string, object> GetSettings()
    {
        var result = DeserializeObject<Dictionary<string, object>>(SettingsFile);
        return result ?? new Dictionary<string, object>();
    }

    public static void SerializeObject<T>(T obj, string fullPath)
    {
        var json = JsonSerializer.Serialize(obj);
        File.WriteAllText(fullPath, json);
    }
    public static T? DeserializeObject<T>(string path)
    {
        var fullPath = Path.Combine(AppContext.BaseDirectory, path);
        var json = File.ReadAllText(fullPath);
        var result = JsonSerializer.Deserialize<T>(json);
        return result;
    }
}