using System;
using System.Collections.Generic;

namespace Day_07_2
{
    class Program
    {
        public static HashSet<Bag> allBags = new HashSet<Bag>();
        static HashSet<Bag> bagWithGold = new HashSet<Bag>();

        static void Main(string[] args)
        {
            Console.WriteLine("AdventOfCode - Day 7-2\n");

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

            //find gold bag
            Bag goldBag = new Bag();
            foreach (Bag b in allBags)
            {
                if (b.name == "shiny gold")
                    goldBag = b;
            }

            short bagAmount = goldBag.StartGetIncludedBagsRecursively();

            Console.WriteLine("\nThere need to be " + bagAmount + " bags in a shiny gold bag.");
        }


    }
}

class Bag
{
    public string name;

    Dictionary<string, short> bagsInsideStrings = new Dictionary<string, short>();
    Dictionary<Bag, short> bagsInside = new Dictionary<Bag, short>();

    public void AddBagString(string bag, short amount)
    {
        bagsInsideStrings.Add(bag, amount);
    }

    public void StringsToBags(HashSet<Bag> allBags)
    {
        foreach (KeyValuePair<string, short> pair in bagsInsideStrings)
        {
            Bag bag = GetBagInList(pair.Key, allBags);
            if (bag != null)
                bagsInside.Add(bag, pair.Value);
        }
        bagsInsideStrings = null;
    }

    public short StartGetIncludedBagsRecursively()
    {
        short amount = 0;

        foreach (KeyValuePair<Bag, short> pair in bagsInside)
        {
            amount += (short)(pair.Key.GetIncludedBagsRecursively() * pair.Value);
        }

        return amount;
    }

    public short GetIncludedBagsRecursively()
    {
        short amount = 1;

        foreach (KeyValuePair<Bag, short> pair in bagsInside)
        {
            amount += (short)(pair.Key.GetIncludedBagsRecursively() * pair.Value);
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

