using System;
using System.Collections.Generic;

namespace Day_1_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("AdventOfCode - Day 1-2\n");

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
                    for (int y = x + 1; y < numbers.Count; y++)
                    {
                        if (numbers[i] + numbers[x] + numbers[y] == 2020)
                        {
                            Console.WriteLine("\nIt is " + numbers[i] + " x " + numbers[x] + " x " + numbers[y] + " = " + numbers[i] * numbers[x] * numbers[y]);
                            return;
                        }
                    }
                }
            }
        }
    }
}
