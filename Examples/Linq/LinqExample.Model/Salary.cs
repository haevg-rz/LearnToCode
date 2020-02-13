using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqExample.Model
{
    public class Salary
    {
        public static IEnumerable<Salary> CreateTestData()
        {
            var rnd = new Random();

            var startDateTime = new DateTime(2000, 1, 1).AddDays(rnd.Next(0, 3000));
            var endDateTime = startDateTime;
            var salary = rnd.Next(2_000, 4_000);

            return Enumerable.Range(0, rnd.Next(1, 12)).Select(_ =>
            {
                startDateTime = endDateTime;
                endDateTime = startDateTime.AddDays(rnd.Next(300, 900));
                salary += rnd.Next(100, 200);
                return new Salary
                {
                    Start = startDateTime,
                    Ende = endDateTime,
                    Money = salary
                };
            });
        }

        public int Money { get; set; }

        public DateTime Ende { get; set; }

        public DateTime Start { get; set; }

        public override string ToString()
        {
            return $"{base.ToString()} from {this.Start} to {this.Ende} with {this.Money:N0}€";
        }
    }
}