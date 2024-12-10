using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fixflow.Model
{
    public static class SettingsManager
    {
        private static readonly string Path = "settings.json";

        public static Settings GetSettings()
        {
            if (!File.Exists(Path))
            {
                var settings = new Settings
                {
                    Something = false,
                    EnableAutoUpdates = false,
                    CheckForUpdates = false
                };
                File.WriteAllText(Path, JsonConvert.SerializeObject(settings, Formatting.Indented));
            }
            var json = File.ReadAllText(Path);
            return JsonConvert.DeserializeObject<Settings>(json)!;
        }

        public static void SaveSettings(Settings settings)
        {
            var json = JsonConvert.SerializeObject(settings, Formatting.Indented);
            File.WriteAllText(Path, json);
            App.Settings = GetSettings();
        }

        public static void UpdateSetting<T>(string propertyName, T value)
        {
            var settings = GetSettings();

            var property = typeof(Settings).GetProperty(propertyName);
            property!.SetValue(settings, value);

            SaveSettings(settings);
        }
    }
}
