using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Utilities
{
    public class Employee
    {
        public string Name { get; set; }
        public string Department { get; set; }
        public int YearsOfExperience { get; set; }

        public static Employee? Parse(string line)
        {
            var parts = line.Split('|').Select(part => part.Trim()).ToArray();
            // Ensure that there are enough parts to avoid IndexOutOfRangeException
            if (parts.Length < 3)
            {
                // Handle the case when there aren't enough parts
                Console.WriteLine($"Invalid line format: {line}");
                return null; 
            }

            // Safely parse each part and handle possible null or empty values
            string name = string.IsNullOrWhiteSpace(parts[0]) ? "" : parts[0];
            string department = string.IsNullOrWhiteSpace(parts[1]) ? "" : parts[1];
            int yearsOfExperience = int.TryParse(parts[2], out int result) ? result : 0;

            return new Employee
            {
                Name = parts[0],
                Department = parts[1],
                YearsOfExperience = int.Parse(parts[2])
            };
        }
    }
}
