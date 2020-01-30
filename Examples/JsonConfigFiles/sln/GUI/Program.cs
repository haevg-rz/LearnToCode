using System;

namespace GUI
{
    class Program
    {
        
        static void Main(string[] args)
        {
            // Instantiate in your main method
            SettingsHelper.Initiate();
            // and access your settings via SettingsHelper.Settings from basically anywhere
            // just be carefule that it might be null if th file was not found/was malformed
            if (SettingsHelper.Settings != null)
            {
                Console.WriteLine(SettingsHelper.Settings.ApiKey);
                Console.WriteLine(SettingsHelper.Settings.Users[1].Email);
            }
            else
            {
                Console.WriteLine("Nothing to output!");
            }

            Console.ReadKey();
        }
    }
}
