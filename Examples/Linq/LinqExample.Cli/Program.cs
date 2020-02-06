using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using LinqExample.Model;

namespace LinqExample.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            var examples = new List<Action>
            {
                Basic_Enumerable,
                Basic_Linq,
                Basic_Select,
                Basic_Where,
                Basic_Group,
                Basic_LargeQuery,
                ForEach,
                ReadCount_Keyword_Any,
                ReadCount_Keyword_Count,
                ReadCount_Keyword_Iteration,
                DeferredExecution,
                AvoidPossibleMultipleEnumeration
            };

            foreach (var example in examples)
            {
                Console.Out.WriteLine($"---- {example.Method.Name} ----");
                example();
            }
        }

        private static void DeferredExecution()
        {
            var employees = DataGenerator.GenerateEmployees();

            Console.Out.WriteLine($"DataGenerator.ReadCount: {DataGenerator.ReadCount}");

            var result = employees.Where(employee => employee.Department.Name == "IT").Take(10).Where(employee => employee.Age>30);

            Console.Out.WriteLine($"DataGenerator.ReadCount: {DataGenerator.ReadCount}");

            var count = result.Count();
            Console.Out.WriteLine("count: "+count);

            Console.Out.WriteLine($"DataGenerator.ReadCount: {DataGenerator.ReadCount}");
        }

        private static void ForEach()
        {
            
        }

        private static void AvoidPossibleMultipleEnumeration()
        {
        }

        private static void Basic_LargeQuery()
        {
        }

        private static void Basic_Group()
        {
            var employees = DataGenerator.GenerateEmployees();
            var groupByDep = employees.GroupBy(employee => employee.Department.Name);
            foreach (var g in groupByDep)
            {
                Console.Out.WriteLine($"Dep: {g.Key}, count: {g.Count()}");
            }

            var averageAgeByDep = employees.GroupBy(employee => employee.Department.Name).Select(g => new{ Department = g.Key, AverageAge = g.Average(employee => employee.Age)});
            foreach (var depAge in averageAgeByDep)
            {
                Console.Out.WriteLine($"Dep: {depAge.Department}, AverageAge: {depAge.AverageAge}");
            }
        }

        private static void Basic_Where()
        {
            var employees = DataGenerator.GenerateEmployees();

            var itPeople = employees.Where(employee => employee.Department.Name == "IT");
            Console.Out.WriteLine("IT people: " + itPeople.Count());

            var searchList = new[] {"Ryan", "Amari", "Eve", "Millie", "Kelly", "Selah", "Lacey", "Willa", "Haylee", "Jaylah"};
            var foundList = employees.Where(employee => searchList.Any(n => employee.Name == n));
            Console.Out.WriteLine("Found by name: " + foundList.Count());
        }

        private static void Basic_Select()
        {
            var employees = DataGenerator.GenerateEmployees();

            var names = employees.Select(employee => employee.Name);
            Console.Out.WriteLine("names count: " + names.Count());

            var saleries1 = employees.Select(employee => employee.Salaries).ToArray();
            Console.Out.WriteLine("saleries1 count: " + saleries1.Length);
            Console.Out.WriteLine("saleries1 type: " + saleries1.GetType());

            var saleries2 = employees.SelectMany(employee => employee.Salaries).ToArray();
            Console.Out.WriteLine("saleries2 count: " + saleries2.Length);
            Console.Out.WriteLine("saleries2 type: " + saleries2.ToArray().GetType());

            var employeesArray = employees.ToArray();
            Console.Out.WriteLine("1st Data:      " + JsonSerializer.Serialize(employeesArray[0]));

            var employeesSubDataset = employeesArray.Select(employee => new {employee.Name, AgeInYears = employee.Age}).ToArray();
            Console.Out.WriteLine("1st SubDataset:" + JsonSerializer.Serialize(employeesSubDataset[0]));
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

        private static void ReadCount_Keyword_Iteration()
        {
            var employees = DataGenerator.GenerateEmployees();

            Console.Out.WriteLine($"DataGenerator.ReadCount: {DataGenerator.ReadCount}");

            var count1 = 0;
            foreach (var _ in employees)
            {
                count1 += 1;
            }

            var count2 = 0;
            foreach (var _ in employees)
            {
                count2 += 1;
            }

            Console.Out.WriteLine($"count1: {count1}");
            Console.Out.WriteLine($"count2: {count2}");
            Console.Out.WriteLine($"DataGenerator.ReadCount: {DataGenerator.ReadCount}");
        }
    }
}