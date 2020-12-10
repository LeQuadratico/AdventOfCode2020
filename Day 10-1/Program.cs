using System;
using System.Collections.Generic;

namespace Day_10_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("AdventOfCode - Day 10-1\n");

            Console.WriteLine("Enter path to textfile:");
            string path = Console.ReadLine();
            Console.WriteLine();
            string[] lines = System.IO.File.ReadAllLines(path);

            List<short> numbers = new List<short>();
            foreach(string line in lines)
            {
                numbers.Add(short.Parse(line));
            }
            //Add outlet
            numbers.Add(0);

            numbers.Sort();

            //add device
            numbers.Add((short)(numbers[numbers.Count - 1] + 3));

            short j1 = 0;
            short j3 = 0;
            for (int i = 0; i < numbers.Count - 1; i++)
            {
                short diff = (short)(numbers[i + 1] - numbers[i]);
                switch (diff)
                {
                    case 1:
                        j1++;
                        break;
                    case 3:
                        j3++;
                        break;
                }
            }

            int result = j1 * j3;

            Console.WriteLine("The result is " + result);
        }
    }
}
