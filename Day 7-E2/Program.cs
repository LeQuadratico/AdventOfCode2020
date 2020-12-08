using System;
using System.Collections.Generic;
using System.Linq;

namespace Day_7_E2
{
    class Program
    {
        public static HashSet<Bag> allBags = new HashSet<Bag>();
        static HashSet<Bag> bagWithGold = new HashSet<Bag>();

        static void Main(string[] args)
        {
            Console.WriteLine("AdventOfCode - Day 7-E2 - Counting the amount of bags each bag contains\n");

            Console.WriteLine("Enter path to textfile:");
            string path = Console.ReadLine();
            string[] lines = System.IO.File.ReadAllLines(path);

            foreach (string line in lines)
            {
                if (line == String.Empty)
                    continue;

                Bag bag = new Bag();

                //add name
                #region name
                string name = String.Empty;
                short pointer = 0;
                bool behindFirstPart = false;
                while (true)
                {
                    if (line[pointer] != ' ')
                    {
                        //in name
                        name += line[pointer];
                        pointer++;
                    }
                    else
                    {
                        if (!behindFirstPart)
                        {
                            behindFirstPart = true;
                            name += " ";
                            pointer++;
                        }
                        else
                        {
                            //behind name
                            bag.name = name;
                            pointer += 14;
                            break;
                        }
                    }
                }
                #endregion

                //Add other bags inside
                while (true)
                {
                    if (pointer >= line.Length)
                        break;

                    if (line[pointer] == 'n')
                    {
                        //contains no bags
                        break;
                    }

                    string s = line[pointer].ToString();
                    short amount = short.Parse(s);

                    pointer += 2;

                    string bagname = string.Empty;
                    behindFirstPart = false;
                    while (true)
                    {
                        if (line[pointer] != ' ' && line[pointer] != ',' && line[pointer] != '.')
                        {
                            //in name
                            bagname += line[pointer];
                            pointer++;
                        }
                        else
                        {
                            if (!behindFirstPart)
                            {
                                behindFirstPart = true;
                                bagname += " ";
                                pointer++;
                            }
                            else
                            {
                                //behind name
                                bag.AddBagString(bagname, amount);
                                if (amount > 1)
                                    pointer += 7;
                                else
                                    pointer += 6;

                                break;
                            }
                        }
                    }
                }

                allBags.Add(bag);
            }

            foreach (Bag b in allBags)
                b.StringsToBags(allBags);


            Dictionary<Bag, long> end = new Dictionary<Bag, long>();
            foreach (Bag b in allBags)
            {
                end.Add(b, b.StartGetIncludedBagsRecursively());
            }

            Console.Write("\n");
            foreach(KeyValuePair<Bag, long> pair in end.OrderByDescending(key => key.Value))
            {
                Console.WriteLine(pair.Key.name + ": " + pair.Value);
            }
        }


    }
}

class Bag
{
    public string name;

    Dictionary<string, long> bagsInsideStrings = new Dictionary<string, long>();
    Dictionary<Bag, long> bagsInside = new Dictionary<Bag, long>();

    public void AddBagString(string bag, long amount)
    {
        bagsInsideStrings.Add(bag, amount);
    }

    public void StringsToBags(HashSet<Bag> allBags)
    {
        foreach (KeyValuePair<string, long> pair in bagsInsideStrings)
        {
            Bag bag = GetBagInList(pair.Key, allBags);
            if (bag != null)
                bagsInside.Add(bag, pair.Value);
        }
        bagsInsideStrings = null;
    }

    public long StartGetIncludedBagsRecursively()
    {
        long amount = 0;

        foreach (KeyValuePair<Bag, long> pair in bagsInside)
        {
            amount += pair.Key.GetIncludedBagsRecursively() * pair.Value;
        }

        return amount;
    }

    public long GetIncludedBagsRecursively()
    {
        long amount = 1;

        foreach (KeyValuePair<Bag, long> pair in bagsInside)
        {
            amount += pair.Key.GetIncludedBagsRecursively() * pair.Value;
        }

        return amount;
    }

    static Bag GetBagInList(string s, HashSet<Bag> allBags)
    {
        foreach (Bag b in allBags)
        {
            if (b.name == s)
                return b;
        }
        return null;
    }
}

