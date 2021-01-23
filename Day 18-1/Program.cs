using System;

namespace Day_18_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("AdventOfCode - Day 18-1\n");

            Console.WriteLine("Enter path to textfile:");
            string path = Console.ReadLine();
            Console.WriteLine();
            string[] lines = System.IO.File.ReadAllLines(path);

            long sum = 0;
            foreach (string line in lines)
            {
                sum += Solve(line).Item1;
            }

            Console.WriteLine("The sum is " + sum);
        }

        static (long, int) Solve(string s)
        {
            long value = 0;
            bool addOperator = true;

            int pointer = 0;

            while (pointer < s.Length)
            {
                int number = 0;
                int pos = 0;
                if (int.TryParse(s[pointer].ToString(), out number))
                {
                    if (addOperator)
                        value += number;
                    else
                        value *= number;
                }
                else if (s[pointer] == '(')
                {
                    (long result, int pos) solveResult = Solve(s.Substring(pointer + 1));

                    if (addOperator)
                        value += solveResult.result;
                    else
                        value *= solveResult.result;

                    pos = solveResult.pos;
                }
                else if (s[pointer] == ')')
                {
                    return (value, pointer + 2);
                }
                else if (s[pointer] == '*')
                {
                    addOperator = false;
                }
                else if (s[pointer] == '+')
                {
                    addOperator = true;
                }

                if (pos != 0)
                {
                    pointer += pos;
                    pos = 0;
                }
                else
                    pointer++;
            }

            return (value, 0);
        }
    }
}
