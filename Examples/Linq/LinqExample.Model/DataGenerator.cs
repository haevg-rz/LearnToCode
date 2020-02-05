using System.Collections.Generic;
using System.Linq;

namespace LinqExample.Model
{
    public static class DataGenerator
    {
        public static IEnumerable<Employee> GenerateEmployees()
        {
            DataGenerator.ReadCount = 0;
            return Enumerable.Range(1, 1_024).Select(i =>
            {
                DataGenerator.ReadCount = i;
                return Employee.CreateTestData();
            });
        }

        public static int ReadCount;
    }
}