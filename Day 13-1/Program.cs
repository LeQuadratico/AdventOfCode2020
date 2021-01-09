using System;
using System.Collections.Generic;

namespace Day_13_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("AdventOfCode - Day 13-1\n");

            Console.WriteLine("Enter path to textfile:");
            string path = Console.ReadLine();
            Console.WriteLine();
            string[] lines = System.IO.File.ReadAllLines(path);

            string startString = string.Empty;
            int pointer = 0;
            while (pointer < lines[0].Length)
            {
                startString += lines[0][pointer];
                pointer++;
            }
            int start = int.Parse(startString);

            List<Bus> busses = new List<Bus>();
            pointer = 0;
            string busString = string.Empty;
            while (pointer < lines[1].Length)
            {
                if (lines[1][pointer] == ',')
                {
                    busses.Add(new Bus(int.Parse(busString)));
                    busString = string.Empty;
                    pointer++;
                    continue;
                }
                if (lines[1][pointer] == 'x')
                {
                    pointer += 2;
                    continue;
                }

                busString += lines[1][pointer];
                pointer++;
                continue;
            }
            if (busString != null)
            {
                busses.Add(new Bus(int.Parse(busString)));
            }

            Dictionary<int, int> results = new Dictionary<int, int>();
            foreach (Bus b in busses)
            {
                (int, int) tresult = b.GetNextBusDeparture(start);
                if (!results.ContainsKey(tresult.Item1))
                    results.Add(tresult.Item1, tresult.Item2);
            }

            int shortestTime = int.MaxValue;
            foreach (KeyValuePair<int, int> pair in results)
            {
                if (pair.Key < shortestTime)
                    shortestTime = pair.Key;
            }

            int id = results[shortestTime];
            int difference = shortestTime - start;

            int result = id * difference;

            Console.WriteLine("The result is " + result);
        }
    }

    class Bus
    {
        public int time;

        public Bus(int time)
        {
            this.time = time;
        }

        public (int, int) GetNextBusDeparture(int target)
        {
            int currentValue = 0;
            while (currentValue < target)
            {
                currentValue += time;
            }
            return (currentValue, time);
        }
    }
}
