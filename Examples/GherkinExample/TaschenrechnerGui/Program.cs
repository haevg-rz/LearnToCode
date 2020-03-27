using System;
using TaschenrechnerCore;

namespace TaschenrechnerGui
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Geben sie die erste Zahl ein: ");
            TaschenrechnerLogik.Zahl1 = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Geben sie die zweite Zahl ein: ");
            TaschenrechnerLogik.Zahl2 = Convert.ToInt32(Console.ReadLine());

            //Simulates pressing the add button
            TaschenrechnerLogik.AddTwoNumbers();

            Console.WriteLine(
                $"Das Ergebnis der Addition der beiden Zahlen ist: {TaschenrechnerLogik.Result}");

            Console.ReadLine();
        }
    }
}