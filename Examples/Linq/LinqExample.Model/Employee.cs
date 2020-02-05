using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqExample.Model
{
    public class Employee
    {
        internal static Employee CreateTestData()
        {
            var rnd = new Random();
            var deps = Department.CreateTestData();

            var names = new[]
            {
                "Ryan",
                "Amari",
                "Eve",
                "Millie",
                "Kelly",
                "Selah",
                "Lacey",
                "Willa",
                "Haylee",
                "Jaylah",
                "Sylvia",
                "Melany",
                "Elisa",
                "Elsa",
                "Hattie",
                "Raven",
                "Holly",
                "Aisha",
                "Itzel",
                "Kyra",
                "Tiffany",
                "Jayda",
                "Michaela",
                "Madilynn",
                "Jamie",
                "Celeste",
                "Lilian",
                "Remi",
                "Priscilla",
                "Jazlyn",
                "Karen",
                "Savanna",
                "Zariah",
                "Lauryn",
                "Alanna",
                "Kara",
                "Karla",
                "Cassandra",
                "Ariah",
                "Evie",
                "Frances",
                "Aileen",
                "Lennon",
                "Charley",
                "Rosemary",
                "Danna",
                "Regina",
                "Kaelyn",
                "Virginia",
                "Hanna",
                "Rebekah",
                "Alani",
                "Edith",
                "Liana",
                "Charleigh",
                "Gloria",
                "Cameron",
                "Colette",
                "Kailey",
                "Carter",
                "Helena",
                "Matilda",
                "Imani",
                "Bridget",
                "Cynthia",
                "Noah",
                "Liam",
                "Mason",
                "Jacob",
                "William",
                "Ethan",
                "James",
                "Alexander",
                "Michael",
                "Christian",
                "Jaxon",
                "Julian",
                "Landon",
                "Grayson",
                "Jonathan",
                "Isaiah",
                "Charles",
                "Thomas",
                "Aaron",
                "Eli",
                "Connor",
                "Jeremiah",
                "Cameron",
                "Josiah",
                "Adrian",
                "Colton",
                "Jordan",
                "Brayden",
                "Nicholas",
                "Robert",
                "Angel",
                "Hudson",
                "Lincoln",
                "Evan",
                "Dominic",
                "Austin",
                "Gavin",
                "Nolan",
                "Parker",
                "Adam",
                "Chase",
                "Jace",
                "Ian",
                "Cooper",
                "Easton",
                "Kevin",
                "Jose",
                "Tyler",
                "Brandon",
                "Asher",
                "Jaxson",
                "Mateo",
                "Jason",
                "Ayden",
                "Zachary",
                "Carson",
            };

            return new Employee
            {
                Name = names.OrderBy(_ => Guid.NewGuid()).First(),
                Salaries = Salary.CreateTestData(),
                Department = deps.OrderBy(department => Guid.NewGuid()).First(),
                Born = DateTime.Now.AddYears(-1 * rnd.Next(20, 60)).AddDays(rnd.Next(1, 365)).Date,
            };
        }

        public string Name { get; set; }

        public int AgeInDays => (int) (DateTime.Now - Born).TotalDays;
        public int Age => (new DateTime(1, 1, 1) + (DateTime.Now - Born)).Year;
        public DateTime Born { get; set; }

        public Department Department { get; set; }

        public IEnumerable<Salary> Salaries { get; set; }

        public override string ToString()
        {
            return $"{base.ToString()} {this.Name} {this.Age} years old, working in Dep: {this.Department?.Name}";
        }
    }
}