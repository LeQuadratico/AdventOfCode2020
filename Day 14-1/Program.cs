using System;
using System.Collections.Generic;

namespace Day_14_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("AdventOfCode - Day 14-1\n");

            Console.WriteLine("Enter path to textfile:");
            string path = Console.ReadLine();
            Console.WriteLine();
            string[] lines = System.IO.File.ReadAllLines(path);

            string mask = String.Empty;
            long[] mem = new long[100000];

            foreach (string line in lines)
            {
                if (line[1] == 'a')
                {
                    //mask
                    mask = line.Substring(7, 36);
                }
                else
                {
                    //mem

                    //mempos
                    string numbers = string.Empty;
                    short pointer = 4;
                    while (true)
                    {
                        if (line[pointer] != ']')
                        {
                            numbers += line[pointer];
                            pointer++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    int memPos = int.Parse(numbers);

                    //value
                    numbers = string.Empty;
                    pointer += 4;
                    while (pointer < line.Length)
                    {
                        numbers += line[pointer];
                        pointer++;
                    }
                    long value = long.Parse(numbers);

                    //apply
                    mem[memPos] = ApplyMask(value, mask);
                }
            }

            ulong sum = 0;
            foreach (ulong l in mem)
            {
                sum += l;
            }

            Console.WriteLine("The sum is " + sum);
        }

        private static long ApplyMask(long input, string mask)
        {
            char[] tempChars = Convert.ToString(input, 2).ToCharArray();

            List<char> inDec = new List<char>();
            inDec.AddRange(tempChars);

            while (inDec.Count < 36)
                inDec.Insert(0, '0');

            for (int i = 0; i < 36; i++)
            {
                if (mask[i] != 'X')
                {
                    inDec[i] = mask[i];
                }
            }
            string finalString = new string(inDec.ToArray());
            return Convert.ToInt64(finalString, 2);
        }
    }
}
