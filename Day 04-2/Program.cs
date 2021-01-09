using System;
using System.Collections.Generic;

namespace Day_04_2
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
            if (currentPassport != null)
                passports.Add(currentPassport);

            int validPasswords = 0;
            foreach (Passport pp in passports)
            {
                if (pp.CheckValidity())
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

            while (true)
            {
                string at = line[pointer].ToString() + line[pointer + 1].ToString() + line[pointer + 2].ToString();
                pointer += 4;

                string text = String.Empty;
                while (true)
                {
                    if (line.Length <= pointer || line[pointer] == ' ')
                    {
                        //behind entry
                        pointer++;
                        break;
                    }
                    else
                    {
                        //in text
                        text += line[pointer];
                        pointer++;
                    }

                }

                AddAttribute(pp, at, text);

                if (line.Length < pointer)
                    break;
            }



            return pp;
        }

        static void AddAttribute(Passport pp, string attribute, string text)
        {
            switch (attribute)
            {
                case "byr":
                    pp.byr = text;
                    break;
                case "iyr":
                    pp.iyr = text;
                    break;
                case "eyr":
                    pp.eyr = text;
                    break;
                case "hgt":
                    pp.hgt = text;
                    break;
                case "hcl":
                    pp.hcl = text;
                    break;
                case "ecl":
                    pp.ecl = text;
                    break;
                case "pid":
                    pp.pid = text;
                    break;
                case "cid":
                    pp.cid = text;
                    break;
            }
        }


    }

    class Passport
    {
        public string byr;
        public string iyr;
        public string eyr;
        public string hgt;
        public string hcl;
        public string ecl;
        public string pid;
        public string cid;

        public bool CheckValidity()
        {
            if (byr != null && iyr != null && eyr != null && hgt != null && hcl != null && ecl != null && pid != null)
            {
                if (CheckBYR() && CheckIYR() && CheckEYR() && CheckHGT() && CheckHCL() && CheckECL() && CheckPID())
                    return true;
            }
            return false;
        }

        bool CheckBYR()
        {
            short date = short.Parse(byr);
            if (byr.Length == 4 && date >= 1920 && date <= 2002)
                return true;
            else
                return false;
        }

        bool CheckIYR()
        {
            short date = short.Parse(iyr);
            if (iyr.Length == 4 && date >= 2010 && date <= 2020)
                return true;
            else
                return false;
        }

        bool CheckEYR()
        {
            short date = short.Parse(eyr);
            if (eyr.Length == 4 && date >= 2020 && date <= 2030)
                return true;
            else
                return false;
        }

        bool CheckHGT()
        {
            bool inCentimeter;
            string ending = hgt[hgt.Length - 2].ToString() + hgt[hgt.Length - 1].ToString();
            if (ending == "cm")
                inCentimeter = true;
            else if (ending == "in")
                inCentimeter = false;
            else
                return false;

            short height = short.Parse(hgt.Remove(hgt.Length - 2));

            if (inCentimeter)
            {
                if (height >= 150 && height <= 193)
                    return true;
            }
            else
            {
                if (height >= 59 && height <= 76)
                    return true;
            }
            return false;
        }

        bool CheckHCL()
        {
            const string allowedChars = "0123456789abcdef";

            short pointer = 0;

            if (!(hcl[pointer] == '#'))
                return false;
            pointer++;

            for (int i = 0; i < 6; i++)
            {
                if (!allowedChars.Contains(hcl[pointer]))
                    return false;
                pointer++;
            }

            if (pointer < hcl.Length - 1)
                return false;

            return true;
        }

        bool CheckECL()
        {
            if (ecl == "amb" || ecl == "blu" || ecl == "brn" || ecl == "gry" || ecl == "grn" || ecl == "hzl" || ecl == "oth")
                return true;
            return false;
        }

        bool CheckPID()
        {
            if (pid.Length == 9)
                return true;
            return false;
        }
    }
}
