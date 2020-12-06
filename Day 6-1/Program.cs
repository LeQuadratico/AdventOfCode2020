using System;
using System.Collections.Generic;

namespace Day_6_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("AdventOfCode - Day 6-1\n");

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
        HashSet<char> yesQuestions = new HashSet<char>();

        public void AddChars(string line)
        {
            char[] chars = line.ToCharArray();
            foreach (char c in chars)
                yesQuestions.Add(c);
        }

        public short YesedQuestions()
        {
            return (short)yesQuestions.Count;
        }
    }
}
