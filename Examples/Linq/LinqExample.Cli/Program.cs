using System;
using System.Linq;
using LinqExample.Model;

namespace LinqExample.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Out.WriteLine(nameof(Basic_Enumerable));
            Basic_Enumerable();

            Console.Out.WriteLine(nameof(Basic_Linq));
            Basic_Linq();

            Console.Out.WriteLine(nameof(Basic_Select));
            Basic_Select();

            Console.Out.WriteLine(nameof(Basic_Where));
            Basic_Where();

            Console.Out.WriteLine(nameof(Basic_Group));
            Basic_Group();

            Console.Out.WriteLine(nameof(Basic_LargeQuery));
            Basic_LargeQuery();

            Console.Out.WriteLine(nameof(DeferredExecutionAndLazyEvaluation));
            DeferredExecutionAndLazyEvaluation();

            Console.Out.WriteLine(nameof(ReadCount_Keyword_Any));
            ReadCount_Keyword_Any();

            Console.Out.WriteLine(nameof(ReadCount_Keyword_Count));
            ReadCount_Keyword_Count();
        }

        private static void Basic_LargeQuery()
        {
        }

        private static void Basic_Group()
        {
        }

        private static void Basic_Where()
        {
        }

        private static void Basic_Select()
        {
        }

        private static void DeferredExecutionAndLazyEvaluation()
        {
        }

        private static void Basic_Enumerable()
        {
            var employees = DataGenerator.GenerateEmployees();
            foreach (var employee in employees)
            {
                Console.Out.WriteLine("employee: " + employee);
                break;
            }

            foreach (var employee in employees)
            {
                Console.Out.WriteLine("employee: " + employee);
                break;
            }
        }

        private static void Basic_Linq()
        {
            var employees = DataGenerator.GenerateEmployees().ToArray();

            // Are there any employees
            Console.Out.WriteLine("Are there any employees? " + employees.Any());

            // Are there any employees older than 20?
            Console.Out.WriteLine("Are there any employees older than 20? " + employees.Any(employee => employee.Age >= 20));

            // Are all employees older than 20?
            Console.Out.WriteLine("Are all employees older than 20? " + employees.All(employee => employee.Age >= 20));

            // What is the youngest age?
            Console.Out.WriteLine("What is the youngest age? " + employees.Min(employee => employee.Age));

            // Who is the youngest?
            Console.Out.WriteLine("Who is the youngest? " + employees.OrderBy(employee => employee.AgeInDays).First());
            // Who is the oldest?
            Console.Out.WriteLine("Who is the oldest? " + employees.OrderBy(employee => employee.AgeInDays).Last());
            Console.Out.WriteLine("Who is the oldest? " + employees.OrderByDescending(employee => employee.AgeInDays).First());
        }

        private static void ReadCount_Keyword_Any()
        {
            var employees = DataGenerator.GenerateEmployees();

            Console.Out.WriteLine($"DataGenerator.ReadCount: {DataGenerator.ReadCount}");

            var hasAny = employees.Any();
            Console.Out.WriteLine($"hasAny: {hasAny}");

            Console.Out.WriteLine($"DataGenerator.ReadCount: {DataGenerator.ReadCount}");
        }

        private static void ReadCount_Keyword_Count()
        {
            var employees = DataGenerator.GenerateEmployees();

            Console.Out.WriteLine($"DataGenerator.ReadCount: {DataGenerator.ReadCount}");

            var count = employees.Count();
            Console.Out.WriteLine($"count: {count}");

            Console.Out.WriteLine($"DataGenerator.ReadCount: {DataGenerator.ReadCount}");
        }
    }
}