using System;
using System.Collections.Generic;

namespace Day_4_1
{
    class Program
    {
        static string[] fulltext;
        static void Main(string[] args)
        {
            Console.WriteLine("AdventOfCode - Day 4-1\n");

            Console.WriteLine("Enter path to textfile:");
            string path = Console.ReadLine();
            fulltext = System.IO.File.ReadAllLines(path);

            List<Passport> passports = new List<Passport>();

            Passport currentPassport = new Passport();

            foreach (string line in fulltext)
            {
                if (line == String.Empty)
                {
                    passports.Add(currentPassport);
                    currentPassport = null;
                }
                else
                {
                    currentPassport = ParsePassport(line, currentPassport);
                }
            }
            if(currentPassport != null)
                passports.Add(currentPassport);

            int validPasswords = 0;
            foreach(Passport pp in passports)
            {
                if (pp.byr && pp.iyr && pp.eyr && pp.hgt && pp.hcl && pp.ecl && pp.pid)
                    validPasswords++;
            }

            Console.WriteLine("\n" + validPasswords + " passwords are valid.");
        }

        static Passport ParsePassport(string line, Passport lastPP)
        {
            Passport pp;
            if (lastPP == null)
                pp = new Passport();
            else
                pp = lastPP;

            List<char> chars = new List<char>();
            chars.AddRange(line.ToCharArray());

            short pointer = 0;

            while(true)
            {
                string at = line[pointer].ToString() + line[pointer + 1].ToString() + line[pointer + 2].ToString();
                AddAttribute(pp, at);
                pointer += 4;

                while(true)
                {
                    if(line.Length <= pointer || line[pointer] == ' ')
                    {
                        pointer++;
                        break;
                    }
                    pointer++;
                }

                if (line.Length < pointer)
                    break;
            }



            return pp;
        }

        static void AddAttribute(Passport pp, string attribute)
        {
            switch (attribute)
            {
                case "byr":
                    pp.byr = true;
                    break;
                case "iyr":
                    pp.iyr = true;
                    break;
                case "eyr":
                    pp.eyr = true;
                    break;
                case "hgt":
                    pp.hgt = true;
                    break;
                case "hcl":
                    pp.hcl = true;
                    break;
                case "ecl":
                    pp.ecl = true;
                    break;
                case "pid":
                    pp.pid = true;
                    break;
                case "cid":
                    pp.cid = true;
                    break;
            }
        }
    }

    class Passport
    {
        public bool byr;
        public bool iyr;
        public bool eyr;
        public bool hgt;
        public bool hcl;
        public bool ecl;
        public bool pid;
        public bool cid;
    }
}
