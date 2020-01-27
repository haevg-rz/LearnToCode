using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace Static
{
    public class Settings
    {
        public const string SettingsPath = @"C:\temp\settings.json";

        public String AppName { get; set; }
        public String[] Maintainer { get; set; }

        public static void SaveStatic(Settings s)
        {
            var jsonSerializerOptions = new JsonSerializerOptions();
            jsonSerializerOptions.WriteIndented = true;
            var json = JsonSerializer.Serialize(s, jsonSerializerOptions);
            File.WriteAllText(SettingsPath, json);
        }

        public void SaveInstance()
        {
            var jsonSerializerOptions = new JsonSerializerOptions();
            jsonSerializerOptions.WriteIndented = true;
            var json = JsonSerializer.Serialize(this, jsonSerializerOptions);
            File.WriteAllText(SettingsPath, json);
        }

        public static Settings Load()
        {
            var json = File.ReadAllText(SettingsPath);
            var settings = JsonSerializer.Deserialize< Settings>(json);
            return settings;
        }
    }
}
