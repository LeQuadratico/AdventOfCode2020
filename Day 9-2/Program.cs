using System;
using System.Collections.Generic;

namespace Day_9_2
{
    class Program
    {
        static ulong[] numbers;

        static void Main(string[] args)
        {
            Console.WriteLine("AdventOfCode - Day 9-2\n");

            Console.WriteLine("Enter path to textfile:");
            string path = Console.ReadLine();
            Console.WriteLine();
            string[] lines = System.IO.File.ReadAllLines(path);

            numbers = new ulong[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                numbers[i] = ulong.Parse(lines[i]);
            }

            ulong problemNumber = GetProblemNumber();

            for (int i = 0; i < numbers.Length; i++)
            {
                List<ulong> resultNumbers = new List<ulong> { numbers[i] };
                ulong result = numbers[i];
                bool foundIt = false;
                for (int x = i + 1; x < numbers.Length; x++)
                {
                    result += numbers[x];

                    if (result > problemNumber)
                        break;
                    else if (result == problemNumber)
                    {
                        //found numbers
                        foundIt = true;
                        break;
                    }
                    else if (result < problemNumber)
                    {
                        resultNumbers.Add(numbers[x]);
                        continue;
                    }
                }

                if(foundIt)
                {
                    //found numbers
                    ulong max = 0;
                    ulong min = ulong.MaxValue;

                    foreach(ulong n in resultNumbers)
                    {
                        if (n < min)
                            min = n;
                        if (n > max)
                            max = n;
                    }

                    ulong fResult = min + max;

                    Console.WriteLine("The searched number is " + fResult);

                    break;
                }
            }
        }


        static ulong GetProblemNumber()
        {
            //same code as Day 9-1
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
                    Console.WriteLine("The problematic number is " + number + "\n");
                    return number;
                }
                pointer++;
            }
        }
    }
}
