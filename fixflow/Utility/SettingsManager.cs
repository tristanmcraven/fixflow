﻿using fixflow.Model;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using Newtonsoft.Json;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Media;


namespace fixflow.Utility
{
    public static class SettingsManager
    {
        private static readonly string Path = "settings.json";

        public static Settings GetSettings()
        {
            if (!File.Exists(Path))
            {
                var defaultSettings = CreateDefaultSettings();
                File.WriteAllText(Path, JsonConvert.SerializeObject(defaultSettings, Formatting.Indented));
            }
            var json = File.ReadAllText(Path);
            var settings = JsonConvert.DeserializeObject<Settings>(json)!;
            var verifiedSettings = VerifySettingsIntegrity(settings);
            SaveSettings(verifiedSettings);
            return verifiedSettings;
        }

        public static void SaveSettings(Settings settings)
        {
            var json = JsonConvert.SerializeObject(settings, Formatting.Indented);
            File.WriteAllText(Path, json);
            App.Settings = settings;
        }

        public static void UpdateSetting<T>(string propertyName, T value)
        {
            var settings = GetSettings();

            var property = typeof(Settings).GetProperty(propertyName);
            property!.SetValue(settings, value);

            SaveSettings(settings);
        }

        public static void SaveWindowSize(string windowName, double width, double height, WindowState state)
        {
            var settings = GetSettings();
            if (settings.RememberWindowSize == true)
            {
                settings.WindowSizes[windowName] = new WindowSize(width, height, state);
                SaveSettings(settings);
            }
        }

        public static void SaveWindowLocation(string windowName, double top, double left, WindowState state)
        {
            var settings = GetSettings();
            if (settings.RememberWindowLocation == true)
            {
                settings.WindowLocations[windowName] = new WindowLocation(top, left, state);
                SaveSettings(settings);
            }
        }

        public static void SaveWindowProperties(Window window)
        {
            if (App.Settings.RememberWindowSize == true) SaveWindowSize(WindowManager.GetName(window), window.ActualWidth, window.ActualHeight, window.WindowState);
            if (App.Settings.RememberWindowLocation == true) SaveWindowLocation(WindowManager.GetName(window), window.Top, window.Left, window.WindowState);
        }

        public static (WindowSize? size, WindowLocation? location) GetWindowSettings(string windowName)
        {
            var settings = GetSettings();

            WindowSize? size = settings.WindowSizes.ContainsKey(windowName)
                ? settings.WindowSizes[windowName]
                : null;

            WindowLocation? location = settings.WindowLocations.ContainsKey(windowName)
                ? settings.WindowLocations[windowName]
                : null;

            return (size, location);
        }

        public static void UpdateAppTheme(AppTheme id)
        {
            var paletteHelper = new PaletteHelper();
            IThemeManager theme = paletteHelper.GetThemeManager();
            if (id == AppTheme.System)
                id = GetSystemTheme();
            switch (id)
            {
                case AppTheme.Light:
                    paletteHelper.SetTheme(Theme.Create(BaseTheme.Light, Colors.Red, Colors.Yellow));
                    break;
                case AppTheme.Dark:
                    paletteHelper.SetTheme(Theme.Create(BaseTheme.Dark, Colors.Red, Colors.Yellow));
                    break;
            }
        }

        private static AppTheme GetSystemTheme()
        {
            const string keyPath = @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";
            const string valueName = "AppsUseLightTheme";

            using (var key = Registry.CurrentUser.OpenSubKey(keyPath))
            {
                if (key?.GetValue(valueName) is int value)
                    return value == 1 ? AppTheme.Light : AppTheme.Dark;
            }

            return AppTheme.Dark;
        }

        private static Settings CreateDefaultSettings()
        {
            return new Settings
            {
                EnableAutoUpdates = false,
                CheckForUpdates = false,
                RememberWindowSize = true,
                RememberWindowLocation = true,
                WindowSizes = new Dictionary<string, WindowSize>(),
                WindowLocations = new Dictionary<string, WindowLocation>(),
                AppTheme = 0,
                AppLanguage = 0
            };
        }

        private static Settings VerifySettingsIntegrity(Settings? settings)
        {
            if (settings == null) return CreateDefaultSettings();
            settings.EnableAutoUpdates ??= false;
            settings.CheckForUpdates ??= false;
            settings.RememberWindowSize ??= true;
            settings.RememberWindowLocation ??= true;

            settings.WindowSizes ??= new Dictionary<string, WindowSize>();
            settings.WindowLocations ??= new Dictionary<string, WindowLocation>();

            settings.AppTheme ??= 0;
            settings.AppLanguage ??= 0;

            return settings;
        }
    }
}
