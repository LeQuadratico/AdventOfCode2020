using System;
using System.Collections.Generic;

namespace Day_16_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("AdventOfCode - Day 16-2\n");

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

                string name = string.Empty;
                while (line[charPointer] != ':')
                {
                    name += line[charPointer];
                    charPointer++;
                }
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

                rules.Add(new Rule(name, startOne, endOne, startTwo, endTwo));

                linePointer++;
            }

            linePointer += 2;
            
            //get myTicket
            Ticket myTicket = new Ticket();
            string myLine = lines[linePointer];
            int myCharPointer = 0;
            string myNs = string.Empty;
            while (myCharPointer < myLine.Length)
            {
                if (myLine[myCharPointer] != ',')
                {
                    myNs += myLine[myCharPointer];
                }
                else
                {
                    myTicket.numbers.Add(short.Parse(myNs));
                    myNs = string.Empty;
                }
                myCharPointer++;
            }
            myTicket.numbers.Add(short.Parse(myNs));
            linePointer += 3;

            //get tickets
            List<Ticket> tickets = new List<Ticket>();
            while (linePointer < lines.Length)
            {
                Ticket ticket = new Ticket();

                string line = lines[linePointer];
                byte charPointer = 0;
                string ns = string.Empty;
                while (charPointer < line.Length)
                {
                    if (line[charPointer] != ',')
                    {
                        ns += line[charPointer];
                    }
                    else
                    {
                        ticket.numbers.Add(short.Parse(ns));
                        ns = string.Empty;
                    }
                    charPointer++;
                }
                ticket.numbers.Add(short.Parse(ns));

                tickets.Add(ticket);

                linePointer++;
            }

            //filter invalid tickets
            for (int i = 0; i < tickets.Count; i++)
            {
                if (!tickets[i].CheckValidity(rules))
                {
                    tickets.RemoveAt(i);
                    i--;
                }
            }

            //calculate possible fields
            foreach (Ticket t in tickets)
            {
                t.CalculatePossibleFiels(rules);
            }

            Dictionary<byte, List<Rule>> possibleRulesOnPos = new Dictionary<byte, List<Rule>>();
            //per rule
            for (byte iRule = 0; iRule < rules.Count; iRule++)
            {
                //per pos
                for (byte iPos = 0; iPos < rules.Count; iPos++)
                {
                    bool posPossible = true;
                    //per ticket
                    for (int iTicket = 0; iTicket < tickets.Count; iTicket++)
                    {
                        if ((rules[iRule].minOne <= tickets[iTicket].numbers[iPos] && tickets[iTicket].numbers[iPos] <= rules[iRule].maxOne)
                                || (rules[iRule].minTwo <= tickets[iTicket].numbers[iPos] && tickets[iTicket].numbers[iPos] <= rules[iRule].maxTwo))
                        {
                            //possible
                        }
                        else
                        {
                            posPossible = false;
                            break;
                        }
                    }
                    if (posPossible)
                    {
                        if (possibleRulesOnPos.ContainsKey(iPos))
                        {
                            possibleRulesOnPos[iPos].Add(rules[iRule]);
                        }
                        else
                        {
                            possibleRulesOnPos.Add(iPos, new List<Rule> { rules[iRule] });
                        }
                    }
                }
            }

            Dictionary<Rule, byte> ruleToPos = new Dictionary<Rule, byte>();
            List<Rule> usedRules = new List<Rule>();
            while (true)
            {
                foreach (KeyValuePair<byte, List<Rule>> pair in possibleRulesOnPos)
                {
                    List<Rule> rs = new List<Rule>();
                    rs.AddRange(pair.Value);
                    foreach (Rule r in usedRules)
                    {
                        rs.Remove(r);
                    }

                    if (rs.Count == 1)
                    {
                        ruleToPos.Add(rs[0], pair.Key);
                        usedRules.Add(rs[0]);
                        break;
                    }
                }
                if(usedRules.Count == rules.Count)
                {
                    break;
                }
            }

            long result = 1;
            foreach(Rule r in rules)
            {
                if(r.name.StartsWith("departure"))
                {
                    result *= myTicket.numbers[ruleToPos[r]];
                }
            }

            Console.WriteLine("The result is " + result);
        }

        class Rule
        {
            public string name;

            public int minOne;
            public int maxOne;
            public int minTwo;
            public int maxTwo;

            public Rule(string name, int minOne, int maxOne, int minTwo, int maxTwo)
            {
                this.name = name;

                this.minOne = minOne;
                this.maxOne = maxOne;
                this.minTwo = minTwo;
                this.maxTwo = maxTwo;
            }
        }

        class Ticket
        {
            public List<short> numbers = new List<short>();
            public Dictionary<byte, List<Rule>> possibleFields = new Dictionary<byte, List<Rule>>();

            public bool CheckValidity(List<Rule> rules)
            {
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
                        return false;
                }
                return true;
            }

            public void CalculatePossibleFiels(List<Rule> rules)
            {
                for (int i = 0; i < numbers.Count; i++)
                {
                    List<Rule> list = new List<Rule>();

                    foreach (Rule r in rules)
                    {
                        if ((r.minOne <= numbers[i] && numbers[i] <= r.maxOne)
                            || (r.minTwo <= numbers[i] && numbers[i] <= r.maxTwo))
                        {
                            list.Add(r);
                        }
                    }

                    possibleFields.Add((byte)i, list);
                }
            }
        }
    }
}