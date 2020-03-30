﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UserControl
{
    public static class Helper
    {
        public static string GetRandomName()
        {
            var names = new List<string>
            {
                "Aiden",
                "Jackson",
                "Mason",
                "Liam",
                "Jacob",
                "Jayden",
                "Ethan",
                "Noah",
                "Lucas",
                "Logan",
                "Caleb",
                "Caden",
                "Jack",
                "Ryan",
                "Connor",
                "Michael",
                "Elijah",
                "Brayden",
                "Benjamin",
                "Nicholas",
                "Alexander",
                "William",
                "Matthew",
                "James",
                "Landon",
                "Nathan",
                "Dylan",
                "Evan",
                "Luke",
                "Andrew",
                "Gabriel",
                "Gavin",
                "Joshua",
                "Owen",
                "Daniel",
                "Carter",
                "Tyler",
                "Cameron",
                "Christian",
                "Wyatt",
                "Henry",
                "Eli",
                "Joseph",
                "Max",
                "Isaac",
                "Samuel",
                "Anthony",
                "Grayson",
                "Zachary",
                "David",
                "Christopher",
                "John",
                "Isaiah",
                "Levi",
                "Jonathan",
                "Oliver",
                "Chase",
                "Cooper",
                "Tristan",
                "Colton",
                "Austin",
                "Colin",
                "Charlie",
                "Dominic",
                "Parker",
                "Hunter",
                "Thomas",
                "Alex",
                "Ian",
                "Jordan",
                "Cole",
                "Julian",
                "Aaron",
                "Carson",
                "Miles",
                "Blake",
                "Brody",
                "Adam",
                "Sebastian",
                "Adrian",
                "Nolan",
                "Sean",
                "Riley",
                "Bentley",
                "Xavier",
                "Hayden",
                "Jeremiah",
                "Jason",
                "Jake",
                "Asher",
                "Micah",
                "Jace",
                "Brandon",
                "Josiah",
                "Hudson",
                "Nathaniel",
                "Bryson",
                "Ryder",
                "Justin",
                "Bryce",
                "Sophia",
                "Emma",
                "Isabella",
                "Olivia",
                "Ava",
                "Lily",
                "Chloe",
                "Madison",
                "Emily",
                "Abigail",
                "Addison",
                "Mia",
                "Madelyn",
                "Ella",
                "Hailey",
                "Kaylee",
                "Avery",
                "Kaitlyn",
                "Riley",
                "Aubrey",
                "Brooklyn",
                "Peyton",
                "Layla",
                "Hannah",
                "Charlotte",
                "Bella",
                "Natalie",
                "Sarah",
                "Grace",
                "Amelia",
                "Kylie",
                "Arianna",
                "Anna",
                "Elizabeth",
                "Sophie",
                "Claire",
                "Lila",
                "Aaliyah",
                "Gabriella",
                "Elise",
                "Lillian",
                "Samantha",
                "Makayla",
                "Audrey",
                "Alyssa",
                "Ellie",
                "Alexis",
                "Isabelle",
                "Savannah",
                "Evelyn",
                "Leah",
                "Keira",
                "Allison",
                "Maya",
                "Lucy",
                "Sydney",
                "Taylor",
                "Molly",
                "Lauren",
                "Harper",
                "Scarlett",
                "Brianna",
                "Victoria",
                "Liliana",
                "Aria",
                "Kayla",
                "Annabelle",
                "Gianna",
                "Kennedy",
                "Stella",
                "Reagan",
                "Julia",
                "Bailey",
                "Alexandra",
                "Jordyn",
                "Nora",
                "Carolin",
                "Mackenzie",
                "Jasmine",
                "Jocelyn",
                "Kendall",
                "Morgan",
                "Nevaeh",
                "Maria",
                "Eva",
                "Juliana",
                "Abby",
                "Alexa",
                "Summer",
                "Brooke",
                "Penelope",
                "Violet",
                "Kate",
                "Hadley",
                "Ashlyn",
                "Sadie",
                "Paige",
                "Katherine",
                "Sienna",
                "Piper"
            };

            return names.OrderBy(_ => Guid.NewGuid()).First();
        }
    }
}
