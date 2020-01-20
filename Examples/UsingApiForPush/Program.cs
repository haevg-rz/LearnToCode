using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;

namespace PushSample
{
    class Program
    {
        private static readonly HttpClient Client = new HttpClient();

        public class Secrets
        {
            public string Token { get; set; }
            public string User { get; set; }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("This is program will send a push message through the Pushover API.");

            if (!TryReadSecrets(out var secrets))
            {
                Environment.ExitCode = -1;
                return;
            }

            var nameValueCollection = new List<KeyValuePair<String, String>>
            {
                KeyValuePair.Create("token", secrets.Token),
                KeyValuePair.Create("user", secrets.User),
                KeyValuePair.Create("html", "1"),
                KeyValuePair.Create("title", "My first push message"),
                KeyValuePair.Create("message", $"Send from <b>{Environment.UserName}</b> on <b>{Environment.MachineName}</b> " +
                                               $"with {Environment.ProcessorCount} CPUs at {DateTime.Now} " +
                                               $"for more information go to <a href=\"https://github.com/haevg-rz/LearnToCode/tree/master/Examples\">github.com</a>"),
            };

            const string url = "https://api.pushover.net/1/messages.json";
            var response = Client.PostAsync(url, new FormUrlEncodedContent(nameValueCollection)).GetAwaiter().GetResult();

            if (response.IsSuccessStatusCode)
            {
                var limitAppRemaining = response.Headers.FirstOrDefault(h => h.Key == "X-Limit-App-Remaining").Value?.FirstOrDefault();
                Console.Out.WriteLine($"Push message was send! {limitAppRemaining} remaining api calls.");
            }
            else
            {
                Console.Out.WriteLine($"Push message wasn't send! Error: {response.StatusCode}");
            }
        }

        private static bool TryReadSecrets(out Secrets secrets)
        {
            secrets = null;

            if (!File.Exists("secrets.json"))
            {
                var sample = new Secrets()
                {
                    Token = "Enter api token here",
                    User = "Enter user id here",
                };
                var settings = new JsonSerializerOptions
                {
                    WriteIndented = true
                };

                File.WriteAllText("secrets.sample.json", JsonSerializer.Serialize(sample, settings));
                Console.Out.WriteLine("Please set credentials in secrets.json, see secrets.sample.json.");
                return false;
            }

            secrets = JsonSerializer.Deserialize<Secrets>(File.ReadAllText("secrets.json"));
            return true;
        }
    }
}
