using System;
using System.Collections.Generic;

namespace Day_19_2
{
    class Program
    {
        static List<Rule> rules = new List<Rule>();
        static Dictionary<short, Rule> idToRule = new Dictionary<short, Rule>();

        static void Main(string[] args)
        {
            Console.WriteLine("AdventOfCode - Day 19-2\n");

            Console.WriteLine("Enter path to textfile:");
            string path = Console.ReadLine();
            Console.WriteLine();
            List<string> lines = new List<string>();
            lines.AddRange(System.IO.File.ReadAllLines(path));

            int lineStartInput = 0;
            foreach (string line in lines)
            {
                if (line == "")
                {
                    lineStartInput = lines.IndexOf(line) + 1;
                    break;
                }
                rules.Add(new Rule(line));
            }

            Rule ruleZero = new Rule("");
            foreach (Rule rule in rules)
            {
                idToRule.Add(rule.id, rule);
                if (rule.id == 0)
                    ruleZero = rule;
            }

            int sum = 0;
            for (int i = lineStartInput; i < lines.Count; i++)
            {
                if (ruleZero.MatchesRuleAsZero(lines[i]))
                    sum++;
            }

            Console.WriteLine("The sum is " + sum);

        }

        class Rule
        {
            public short id;

            char? ruleChar;
            List<List<short>> possibleRulesShort = new List<List<short>>();

            public Rule(string input)
            {
                short possNum = 0;
                short pointer = 0;
                string readString = "";

                possibleRulesShort.Add(new List<short>());

                while (pointer < input.Length)
                {
                    if (input[pointer] == ' ')
                    {
                        possibleRulesShort[possNum].Add(short.Parse(readString));
                        readString = "";
                        pointer++;
                    }
                    else if (input[pointer] == ':')
                    {
                        id = short.Parse(readString);
                        readString = "";
                        pointer += 2;
                    }
                    else if (input[pointer] == '|')
                    {
                        possNum++;
                        possibleRulesShort.Add(new List<short>());
                        pointer += 2;
                    }
                    else if (input[pointer] == '"')
                    {
                        ruleChar = input[pointer + 1];
                        break;
                    }
                    else
                    {
                        readString += input[pointer];
                        pointer++;
                    }
                }
                if (readString != "")
                    possibleRulesShort[possNum].Add(short.Parse(readString));
            }

            public void PrintDebug()
            {
                Console.Write(id + ": " + ruleChar);
                foreach (List<short> list in possibleRulesShort)
                {
                    foreach (short s in list)
                    {
                        Console.Write(" " + s.ToString());
                    }
                    Console.Write(" |");
                }
                Console.Write("\n");
            }

            public CheckResult MatchesRule(string input)
            {
                if (input == "")
                    return new CheckResult(false, 1);

                if (ruleChar != null)
                {
                    if (input[0] == ruleChar)
                    {
                        return new CheckResult(true, 1);
                    }
                    return new CheckResult(false, 1);
                }

                foreach (List<short> list in possibleRulesShort)
                {
                    CheckResult result1 = idToRule[list[0]].MatchesRule(input);
                    if (result1.success)
                    {
                        if (list.Count < 2)
                        {
                            return new CheckResult(true, result1.length);
                        }

                        CheckResult result2 = idToRule[list[1]].MatchesRule(input.Substring(result1.length));
                        if (result2.success)
                        {
                            return new CheckResult(true, (short)(result1.length + result2.length));
                        }
                    }
                }
                return new CheckResult(false, 1);
            }

            public bool MatchesRuleAsZero(string input)
            {
                List<short> possibleBreaks = new List<short>();

                possibleBreaks.AddRange(idToRule[8].MatchesRuleAs8(input));

                foreach (short s in possibleBreaks)
                {
                    CheckResult result = idToRule[11].MatchesRuleAs11(input.Substring(s));
                    if (result.success && result.length + s == input.Length)
                        return true;
                }

                return false;
            }

            List<short> MatchesRuleAs8(string input)
            {
                List<short> possibleBreaks = new List<short>();

                CheckResult result = idToRule[42].MatchesRule(input);
                if (result.success)
                {
                    possibleBreaks.Add(result.length);

                    List<short> newList = MatchesRuleAs8(input.Substring(result.length));
                    for (int i = 0; i < newList.Count; i++)
                        newList[i] += result.length;

                    possibleBreaks.AddRange(newList);
                }
                return possibleBreaks;
            }

            CheckResult MatchesRuleAs11(string input)
            {
                if (input == "")
                    return new CheckResult(false, 1);

                CheckResult result1 = idToRule[42].MatchesRule(input);
                if (result1.success)
                {
                    CheckResult result2 = idToRule[31].MatchesRule(input.Substring(result1.length));
                    if (result2.success)
                        return new CheckResult(true, (short)(result1.length + result2.length));

                    CheckResult resultSelf = MatchesRuleAs11(input.Substring(result1.length));
                    if (resultSelf.success)
                    {
                        result2 = idToRule[31].MatchesRule(input.Substring(result1.length + resultSelf.length));
                        if (result2.success)
                            return new CheckResult(true, (short)(result1.length + result2.length + resultSelf.length));
                        else
                            return new CheckResult(false, 1);
                    }
                }

                return new CheckResult(false, 1);
            }
        }

        struct CheckResult
        {
            public short length;
            public bool success;

            public CheckResult(bool success, short length)
            {
                this.success = success;
                this.length = length;
            }
        }
    }


}
