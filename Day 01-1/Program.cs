using System;
using System.Collections.Generic;

namespace Day_01_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("AdventOfCode - Day 1-1\n");

            Console.WriteLine("Enter path to textfile:");
            string path = Console.ReadLine();

            List<int> numbers = new List<int>();
            foreach (string line in System.IO.File.ReadAllLines(path))
            {
                numbers.Add(int.Parse(line));
            }

            for (int i = 0; i < numbers.Count; i++)
            {
                for (int x = i + 1; x < numbers.Count; x++)
                {
                    if (numbers[i] + numbers[x] == 2020)
                    {
                        Console.WriteLine("\nIt is " + numbers[i] + " x " + numbers[x] + " = " + numbers[i] * numbers[x]);
                        return;
                    }
                }
            }
        }
    }
}
