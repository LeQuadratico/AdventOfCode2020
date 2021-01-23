using System;
using System.Collections.Generic;

namespace Day_18_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("AdventOfCode - Day 18-2\n");

            Console.WriteLine("Enter path to textfile:");
            string path = Console.ReadLine();
            Console.WriteLine();
            string[] lines = System.IO.File.ReadAllLines(path);


            ulong sum = 0;
            foreach (string line in lines)
            {
                List<Number> numbers = new List<Number>();
                Dictionary<int, Number> numberAtDepth = new Dictionary<int, Number>();
                int pointer = 0;
                int depth = 0;

                numberAtDepth.Add(depth, new Number());
                numbers.Add(numberAtDepth[depth]);

                while (pointer < line.Length)
                {
                    if (line[pointer] == '(')
                    {
                        numberAtDepth.Add(depth + 1, new Number(numberAtDepth[depth], depth + 1));
                        depth++;
                    }
                    else if (line[pointer] == ')')
                    {
                        numberAtDepth[depth].s += line[pointer];
                        numbers.Add(numberAtDepth[depth]);
                        numberAtDepth.Remove(depth);
                        depth--;
                    }

                    for (int i = 0; i < depth + 1; i++)
                    {
                        numberAtDepth[depth - i].s += line[pointer];
                    }

                    pointer++;
                }



                int deepest = 0;

                foreach (Number n in numbers)
                    if (n.depth > deepest)
                        deepest = n.depth;

                while (true)
                {
                    foreach (Number n in numbers)
                    {
                        if (n.depth != deepest)
                            continue;

                        n.SolveAndCorrectParent();
                    }
                    deepest--;
                    if (deepest == -1)
                        break;
                }

                sum += (ulong)numberAtDepth[0].value;

                //Console.WriteLine(numberAtDepth[0].value);

            }

            Console.WriteLine("The sum is " + sum);
        }

        class Number
        {
            public string s = string.Empty;
            public string startString;
            public int depth;
            public Number parent;
            public long value;

            public Number(Number parent, int depth)
            {
                this.depth = depth;
                this.parent = parent;
            }

            public Number()
            {
                depth = 0;
            }

            public void SolveAndCorrectParent()
            {
                string debugStart = s;

                if (long.TryParse(s, out _))
                    return;

                if (startString == null)
                    startString = s;

                bool addOperator = false;
                int pointer = 0;
                long lastNumber = -1;
                string numberstring = string.Empty;
                //Additions
                while (pointer < s.Length)
                {
                    long number = 0;

                    if (long.TryParse(s[pointer].ToString(), out number))
                    {
                        numberstring += s[pointer];
                    }
                    else if (s[pointer] == '*')
                    {
                        addOperator = false;
                    }
                    else if (s[pointer] == '+')
                    {
                        addOperator = true;
                    }
                    else if (s[pointer] == ' ')
                    {
                        if (numberstring != string.Empty)
                        {
                            number = long.Parse(numberstring);
                            numberstring = string.Empty;

                            if (addOperator && lastNumber != -1)
                            {
                                s = s.Replace((lastNumber + " + " + number), (lastNumber + number).ToString());
                                pointer = 0;
                                lastNumber = -1;
                                addOperator = false;
                                continue;
                            }
                            else
                                lastNumber = number;
                        }
                    }

                    pointer++;
                }
                if (numberstring != string.Empty)
                {
                    long number = long.Parse(numberstring);
                    numberstring = string.Empty;

                    if (addOperator && lastNumber != -1)
                    {
                        s = s.Replace((lastNumber + " + " + number), (lastNumber + number).ToString());
                        pointer = 0;
                        lastNumber = -1;
                        addOperator = false;
                    }
                    else
                        lastNumber = number;
                }

                //Multiplications
                pointer = 0;
                addOperator = true;
                lastNumber = -1;
                numberstring = string.Empty;
                while (pointer < s.Length)
                {
                    long number = 0;
                    if (long.TryParse(s[pointer].ToString(), out number))
                    {
                        numberstring += s[pointer];
                    }
                    else if (s[pointer] == '*')
                    {
                        addOperator = false;
                    }
                    else if (s[pointer] == '+')
                    {
                        addOperator = true;
                    }
                    else if (s[pointer] == ' ')
                    {
                        if (numberstring != string.Empty)
                        {
                            number = long.Parse(numberstring);
                            numberstring = string.Empty;

                            if (!addOperator && lastNumber != -1)
                            {
                                s = s.Replace((lastNumber + " * " + number), (lastNumber * number).ToString());
                                lastNumber = -1;
                                pointer = 0;
                                addOperator = true;
                                continue;
                            }
                            else
                                lastNumber = number;
                        }
                    }

                    pointer++;
                }
                if (numberstring != string.Empty)
                {
                    long number = long.Parse(numberstring);
                    numberstring = string.Empty;

                    if (!addOperator && lastNumber != -1)
                    {
                        s = s.Replace((lastNumber + " * " + number), (lastNumber * number).ToString());
                        lastNumber = -1;
                        pointer = 0;
                        addOperator = true;
                    }
                    else
                        lastNumber = number;
                }

                if (parent != null)
                {
                    s = s.Substring(1);
                    s = s.Remove(s.Length - 1);
                }

                if (parent != null)
                    parent.CorrentPart(startString, long.Parse(s));
                else
                    value = long.Parse(s);

            }

            public void CorrentPart(string s, long result)
            {
                if (startString == null)
                    startString = this.s;

                this.s = this.s.Replace(s, result.ToString());
            }
        }
    }
}
