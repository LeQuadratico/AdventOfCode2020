using System;

namespace Day_08_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("AdventOfCode - Day 8-1\n");

            Console.WriteLine("Enter path to textfile:");
            string path = Console.ReadLine();
            string[] lines = System.IO.File.ReadAllLines(path);

            int accumulator = 0;
            bool[] alreadyRun = new bool[lines.Length];

            int pointer = 0;
            while (true)
            {
                if (alreadyRun[pointer])
                    break;
                else
                    alreadyRun[pointer] = true;

                string task = lines[pointer][0].ToString() + lines[pointer][1].ToString() + lines[pointer][2].ToString();
                char sign = lines[pointer][4];

                short charPointer = 5;
                string numberString = String.Empty;
                while(charPointer < lines[pointer].Length)
                {
                    numberString += lines[pointer][charPointer];
                    charPointer++;
                }
                short number = short.Parse(numberString);
                if (sign == '-')
                    number *= -1;

                switch(task)
                {
                    case "acc":
                        accumulator += number;
                        pointer++;
                        break;
                    case "jmp":
                        pointer += number;
                        break;
                    case "nop":
                        pointer++;
                        break;
                }
            }

            Console.WriteLine("\nThe accumulator is " + accumulator);
        }
    }
}