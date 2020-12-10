using System;
using System.Collections.Generic;

namespace Day_10_E2
{
    class Program
    {
        public static List<Adapter> adapters = new List<Adapter>();
        static void Main(string[] args)
        {
            Console.WriteLine("AdventOfCode - Day 10-E2 - BruteForce\n");

            Console.WriteLine("Enter path to textfile:");
            string path = Console.ReadLine();
            Console.WriteLine();
            string[] lines = System.IO.File.ReadAllLines(path);

            List<short> numbers = new List<short>();
            foreach (string line in lines)
            {
                if (line != null)
                    numbers.Add(short.Parse(line));
            }
            //Add outlet
            numbers.Add(0);

            numbers.Sort();

            //add device
            numbers.Add((short)(numbers[numbers.Count - 1] + 3));

            foreach (short s in numbers)
            {
                adapters.Add(new Adapter(s, (short)adapters.Count));
            }

            System.Threading.Timer timer = null;
            timer = new System.Threading.Timer((obj) =>
            {
                PrintTime();
                timer.Dispose();
            }, null, 60000, System.Threading.Timeout.Infinite);

            ulong result = adapters[0].GetPossibleCombinations();

            Console.WriteLine("The result is " + result);
        }

        static void PrintTime()
        {
            Console.WriteLine("Average " + Adapter.progress + " solutions per 60 seconds");
            Adapter.progress = 0;

            System.Threading.Timer timer = null;
            timer = new System.Threading.Timer((obj) =>
            {
                PrintTime();
                timer.Dispose();
            }, null, 60000, System.Threading.Timeout.Infinite);
        }
    }

    class Adapter
    {
        public static ulong progress = 0;

        public short value;
        short index;

        public Adapter(short value, short index)
        {
            this.value = value;
            this.index = index;
        }

        public ulong GetPossibleCombinations()
        {
            List<Adapter> adapters = Program.adapters;

            ulong combis = 0;
            for (int i = 1; (i < 4 && index + i < adapters.Count); i++)
            {
                if (adapters[index + i].value - value <= 3)
                {
                    combis += adapters[index + i].GetPossibleCombinations();
                }
            }
            if (combis == 0)
            {
                progress++;
                return 1;
            }
            return combis;
        }
    }
}
