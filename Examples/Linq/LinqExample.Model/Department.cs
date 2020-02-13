using System.Collections.Generic;

namespace LinqExample.Model
{
    public class Department
    {
        public string Name { get; set; }

        public static IEnumerable<Department> CreateTestData()
        {
            return new[]
            {
                new Department {Name = "IT"},
                new Department {Name = "HR"},
                new Department {Name = "Dev"},
                new Department {Name = "Managment"},
                new Department {Name = "Sales"},
            };
        }
    }
}