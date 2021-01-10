using System;
using System.Collections.Generic;

namespace Day_16_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("AdventOfCode - Day 16-1\n");

            Console.WriteLine("Enter path to textfile:");
            string path = Console.ReadLine();
            Console.WriteLine();
            string[] lines = System.IO.File.ReadAllLines(path);

            //get rules
            int linePointer = 0;
            List<Rule> rules = new List<Rule>();
            while (lines[linePointer] != string.Empty)
            {
                string line = lines[linePointer];
                int charPointer = 0;

                while (line[charPointer] != ':')
                    charPointer++;
                charPointer += 2;

                string ns = string.Empty;
                while (line[charPointer] != '-')
                {
                    ns += line[charPointer];
                    charPointer++;
                }
                charPointer++;
                int startOne = int.Parse(ns);

                ns = string.Empty;
                while (line[charPointer] != ' ')
                {
                    ns += line[charPointer];
                    charPointer++;
                }
                charPointer += 4;
                int endOne = int.Parse(ns);

                ns = string.Empty;
                while (line[charPointer] != '-')
                {
                    ns += line[charPointer];
                    charPointer++;
                }
                charPointer++;
                int startTwo = int.Parse(ns);

                ns = string.Empty;
                while (charPointer < line.Length)
                {
                    ns += line[charPointer];
                    charPointer++;
                }
                int endTwo = int.Parse(ns);

                rules.Add(new Rule(startOne, endOne, startTwo, endTwo));

                linePointer++;
            }

            linePointer += 5;

            //get tickets
            List<Ticket> tickets = new List<Ticket>();
            while (linePointer < lines.Length)
            {
                Ticket ticket = new Ticket();

                string line = lines[linePointer];
                int charPointer = 0;
                string ns = string.Empty;
                while (charPointer < line.Length)
                {
                    if (line[charPointer] != ',')
                    {
                        ns += line[charPointer];
                    }
                    else
                    {
                        ticket.numbers.Add(int.Parse(ns));
                        ns = string.Empty;
                    }
                    charPointer++;
                }
                ticket.numbers.Add(int.Parse(ns));

                tickets.Add(ticket);

                linePointer++;
            }

            int errors = 0;
            foreach (Ticket t in tickets)
            {
                errors += t.CheckValidity(rules);
            }

            Console.WriteLine("There are " + errors + " errors");
        }

        class Rule
        {
            public int minOne;
            public int maxOne;
            public int minTwo;
            public int maxTwo;

            public Rule(int minOne, int maxOne, int minTwo, int maxTwo)
            {
                this.minOne = minOne;
                this.maxOne = maxOne;
                this.minTwo = minTwo;
                this.maxTwo = maxTwo;
            }
        }

        class Ticket
        {
            public List<int> numbers = new List<int>();

            public int CheckValidity(List<Rule> rules)
            {
                int errors = 0;
                foreach (int number in numbers)
                {
                    bool valid = false;
                    foreach (Rule r in rules)
                    {
                        if ((r.minOne <= number && number <= r.maxOne)
                            || (r.minTwo <= number && number <= r.maxTwo))
                        {
                            valid = true;
                            break;
                        }
                    }
                    if (!valid)
                        errors += number;
                }
                return errors;
            }
        }
    }
}