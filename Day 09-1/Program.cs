using System;

namespace Day_09_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("AdventOfCode - Day 9-1\n");

            Console.WriteLine("Enter path to textfile:");
            string path = Console.ReadLine();
            Console.WriteLine();
            string[] lines = System.IO.File.ReadAllLines(path);

            ulong[] numbers = new ulong[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                numbers[i] = ulong.Parse(lines[i]);
            }

            int pointer = 25;
            while (true)
            {
                ulong number = numbers[pointer];
                bool valid = false;

                for (int i = 1; i < 25; i++)
                {
                    for (int x = 1; x < 26 - i; x++)
                    {
                        if (numbers[pointer - i] != numbers[pointer - i - x] && numbers[pointer - i] + numbers[pointer - i - x] == number)
                        {
                            valid = true;
                            break;
                        }
                    }

                    if (valid)
                        break;
                }

                if (!valid)
                {
                    Console.WriteLine("The number is " + number);
                    break;
                }
                pointer++;
            }
        }
    }
}
