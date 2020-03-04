using System;

namespace BinarySearch
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Binary search test:");
            int[] array = new[] { 1, 2, 5, 8, 15, 45, 87, 99 };
            int valueToSearch = 6;
            int position = SearchBinary(array, valueToSearch);
            if (position == -1)
            {
                Console.WriteLine($"Could not find value {valueToSearch}");
            }
            else
            {
                Console.WriteLine($"Value {valueToSearch} found at position {position}");
            }

            Console.ReadKey();
        }

        private static int SearchBinary(int[] array, int searchValue)
        {
            int min = 0;
            int max = array.Length - 1;
            while (min <= max)
            {
                int mid = (min + max) / 2;
                if (searchValue == array[mid])
                {
                    return mid;
                }
                else if (searchValue < array[mid])
                {
                    max = mid - 1;
                }
                else
                {
                    min = mid + 1;
                }
            }

            return -1;
        }

    }
}
