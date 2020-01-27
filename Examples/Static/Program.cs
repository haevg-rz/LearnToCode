using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace Static
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Out.WriteLine(nameof(CounterSample));
            CounterSample();

            Console.Out.WriteLine(nameof(CounterStaticSample));
            CounterStaticSample();

            // CS0120 C# An object reference is required for the non-static field, method, or property
            // MyStupidMethod();
            // Program.MyStupidMethod();

            var program = new Program();
            program.MyStupidMethod();

            // var fi = new FileInfo(@"C:\Windows");
            // File.ReadAllText(null);

            Console.Out.WriteLine(nameof(SettingsSample));
            SettingsSample();

            var passwd = "my secret passwd #1";
            var input = Encoding.UTF8.GetBytes(passwd);

            // in real life use SHA256.Create()
            var hash = new SHA256Managed().ComputeHash(input);
            Console.Out.WriteLine("input: " + passwd);
            Console.Out.WriteLine("hash:  " + Convert.ToBase64String(hash));

            hash = SHA512.Create().ComputeHash(input);
            Console.Out.WriteLine("input: " + passwd);
            Console.Out.WriteLine("hash:  " + Convert.ToBase64String(hash));
            Console.Out.WriteLine("hash:  " + BitConverter.ToString(hash));
        }

        private static void SettingsSample()
        {
            var s1 = new Settings();
            s1.AppName = "My App";
            s1.Maintainer = new[] {"David", "Günter"};

            s1.SaveInstance();
            Settings.SaveStatic(s1);

            var s2 = Settings.Load();
            Console.Out.WriteLine("AppName: " + s2.AppName);
        }

        public void MyStupidMethod()
        {
        }

        private static void CounterSample()
        {
            var c1 = new Counter();
            var c2 = new Counter();

            c1.Add(5);
            c2.Add(2);
            c1.Add(1);

            Console.Out.WriteLine("c1: " + c1.counter);
            Console.Out.WriteLine("c2: " + c2.counter);
        }

        private static void CounterStaticSample()
        {
            CounterStatic.counterInternal = -1;

            var c1 = new CounterStatic();
            var c2 = new CounterStatic();

            c1.Add(5);
            c2.Add(2);
            c1.Add(1);

            Console.Out.WriteLine("c1: " + c1.counter);
            Console.Out.WriteLine("c2: " + c2.counter);
        }
    }
}