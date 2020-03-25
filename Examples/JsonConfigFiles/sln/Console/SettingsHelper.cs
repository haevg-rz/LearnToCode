using System;
using System.IO;
using GUI.Model;
using Newtonsoft.Json;

namespace GUI
{ 
    public static class SettingsHelper
    {
        public static Settings Settings { get; set; } = null;

        public static void Initiate()
        {
            if (Settings == null)
            {
                Settings = GetSettings();
            }
        }

        private static Settings GetSettings()
        {
            // initialize an object in case the config file does not exist or is malformed
            Settings settings = new Settings();
            // where is the json file located?
            // here, we use a subfolder with the name of the project "JsonConfigFiles" as a folder
            // for our configuration file(s)
            // 
            // This will result in a path like this:
            // Windows: C:\Users\<YourUsername>\AppData\Roaming\JsonConfigFiles
            // Linux: /home/<YourUsername>/.config/JsonConfigFiles (only with .NET Core, not .NET Framework)
            string appDatapath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + Path.DirectorySeparatorChar + "JsonConfigFiles";
            // combine path and filename
            string filePath = Path.Combine(appDatapath, "settings.json");
            // if the config file does not exist, return null
            if (!File.Exists(filePath))
                return null;
            // read all contents from the json file into a string variable
            string jsonString = File.ReadAllText(filePath);
            // deserialize the string into a usable object
            // which will be an instance of Settings (see Model.Settings)
            settings = JsonConvert.DeserializeObject<Settings>(jsonString);
            // return the object
            return settings;
        }

        public static void SaveSettings()
        {
            // where is the json file located?
            // here, we use a subfolder with the name of the project "JsonConfigFiles" as a folder
            // for our configuration file(s)
            // 
            // This will result in a path like this:
            // Windows: C:\Users\<YourUsername>\AppData\Roaming\JsonConfigFiles
            // Linux: /home/<YourUsername>/.config/JsonConfigFiles (only with .NET Core, not .NET Framework)
            string appDatapath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + Path.DirectorySeparatorChar + "JsonConfigFiles";
            // If our folder does not exist, create it
            if (!Directory.Exists(appDatapath))
                Directory.CreateDirectory(appDatapath);
            // combine path and filename
            string filePath = Path.Combine(appDatapath, "settings.json");
            // serialize the object into a string
            string jsonString = JsonConvert.SerializeObject(Settings);
            // write the generated json string into the settings file
            File.WriteAllText(filePath, jsonString);
        }
    }
}
