using System;
using System.Collections.Generic;

namespace Day_14_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("AdventOfCode - Day 14-2\n");

            Console.WriteLine("Enter path to textfile:");
            string path = Console.ReadLine();
            Console.WriteLine();
            string[] lines = System.IO.File.ReadAllLines(path);

            string mask = String.Empty;
            Dictionary<ulong, ulong> mem = new Dictionary<ulong, ulong>();

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
                    List<long> addresses = ApplyMask(memPos, mask);

                    //value
                    numbers = string.Empty;
                    pointer += 4;
                    while (pointer < line.Length)
                    {
                        numbers += line[pointer];
                        pointer++;
                    }
                    ulong value = ulong.Parse(numbers);

                    //apply
                    foreach (ulong adress in addresses)
                        mem[adress] = value;
                }
            }

            ulong sum = 0;
            foreach (KeyValuePair<ulong, ulong> pair in mem)
            {
                sum += pair.Value;
            }

            Console.WriteLine("The sum is " + sum);
        }

        private static List<long> ApplyMask(int input, string mask)
        {
            char[] tempChars = Convert.ToString(input, 2).ToCharArray();

            List<char> inDec = new List<char>();
            inDec.AddRange(tempChars);

            while (inDec.Count < 36)
                inDec.Insert(0, '0');

            for (int i = 0; i < 36; i++)
            {
                if (mask[i] != '0')
                {
                    inDec[i] = mask[i];
                }
            }

            List<long> addresses = new List<long>();

            AddPossibilities(inDec);

            void AddPossibilities(List<char> chars)
            {
                List<char> newList = new List<char>();
                newList.AddRange(chars);

                for (int i = 0; i < newList.Count; i++)
                {
                    if (newList[i] == 'X')
                    {
                        newList[i] = '0';
                        AddPossibilities(newList);
                        newList[i] = '1';
                        AddPossibilities(newList);
                        return;
                    }
                }

                addresses.Add(Convert.ToInt64(new string(chars.ToArray()), 2));

            }            
            return addresses;
        }
    }
}
