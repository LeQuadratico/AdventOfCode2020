using System;
using System.Collections.Generic;

namespace Day_15_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("AdventOfCode - Day 15-1\n");

            Console.WriteLine("Enter path to textfile:");
            string path = Console.ReadLine();
            Console.WriteLine();
            string line = System.IO.File.ReadAllLines(path)[0];

            List<int> numbers = new List<int>();

            int pointer = 0;
            string number = string.Empty;
            while (pointer < line.Length)
            {
                if (line[pointer] != ',')
                    number += line[pointer];
                else
                {
                    numbers.Add(byte.Parse(number));
                    number = string.Empty;
                }
                pointer++;
            }
            numbers.Add(byte.Parse(number));

            Dictionary<int, int> lastAppeared = new Dictionary<int, int>();
            for (int i = 0; i < numbers.Count - 1; i++)
            {
                lastAppeared.Add(numbers[i], i);
            }

            for (int i = numbers.Count; i < 2020; i++)
            {
                int c = numbers[i - 1];

                if (lastAppeared.ContainsKey(c))
                {
                    numbers.Add(i - 1 - lastAppeared[c]);
                }
                else
                {
                    numbers.Add(0);
                }
                lastAppeared[c] = i - 1;
            }

            Console.WriteLine("The 2020th number is " + numbers[2019]);
        }
    }
}
