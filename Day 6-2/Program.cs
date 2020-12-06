using System;
using System.Collections.Generic;

namespace Day_6_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("AdventOfCode - Day 6-2\n");

            Console.WriteLine("Enter path to textfile:");
            string path = Console.ReadLine();
            string[] lines = System.IO.File.ReadAllLines(path);

            List<Group> groups = new List<Group>();
            Group currentGroup = new Group();
            foreach (string line in lines)
            {
                if (line == String.Empty)
                {
                    groups.Add(currentGroup);
                    currentGroup = null;
                    continue;
                }

                if (currentGroup == null)
                    currentGroup = new Group();

                currentGroup.AddChars(line);
            }
            if (currentGroup != null)
            {
                groups.Add(currentGroup);
            }

            int sum = 0;
            foreach (Group g in groups)
            {
                sum += g.YesedQuestions();
            }

            Console.WriteLine("\nThe sum is " + sum + ".");
        }
    }

    class Group
    {
        Dictionary<char, short> yesQuestions = new Dictionary<char, short>();
        short members = 0;

        public void AddChars(string line)
        {
            char[] chars = line.ToCharArray();
            foreach (char c in chars)
            {
                if (yesQuestions.ContainsKey(c))
                    yesQuestions[c]++;
                else
                    yesQuestions.Add(c, 1);
            }

            members++;
        }

        public short YesedQuestions()
        {
            short count = 0;

            foreach(KeyValuePair<char, short> pair in yesQuestions)
            {
                if (pair.Value == members)
                    count++;
            }

            return count;
        }
    }
}
