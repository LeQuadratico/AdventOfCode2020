using System;
using System.Collections.Generic;

namespace Day_2_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("AdventOfCode - Day 2-1\n");

            Console.WriteLine("Enter path to textfile:");
            string path = Console.ReadLine();

            List<Password> passwords = new List<Password>();
            foreach (string line in System.IO.File.ReadAllLines(path))
            {
                passwords.Add(ParsePassword(line));
            }

            int validPasswords = 0;

            foreach(Password pw in passwords)
            {
                if (CheckPassword(pw))
                    validPasswords++;
            }

            Console.WriteLine("\n" + validPasswords + " passwords are valid!");
        }

        static Password ParsePassword(string line)
        {
            Password newPassword = new Password();

            List<char> chars = new List<char>();
            chars.AddRange(line.ToCharArray());

            short pointer = 0;

            string min = chars[pointer].ToString();
            pointer++;
            if (chars[pointer] == '-')
                pointer++;
            else
            {
                min += chars[pointer];
                pointer += 2;
            }
            newPassword.min = short.Parse(min);

            string max = chars[pointer].ToString();
            pointer++;
            if (chars[pointer] == ' ')
                pointer++;
            else
            {
                max += chars[pointer];
                pointer += 2;
            }
            newPassword.max = short.Parse(max);

            newPassword.letter = chars[pointer];
            pointer += 3;

            string password = string.Empty;
            for (int i = pointer; i < chars.Count; i++)
            {
                password += chars[i];
            }

            newPassword.password = password;

            return newPassword;
        }

        static bool CheckPassword(Password pw)
        {
            short letterAmount = 0;
            for (int i = 0; i < pw.password.Length; i++)
            {
                if (pw.password[i] == pw.letter)
                    letterAmount++;
            }

            if (letterAmount >= pw.min && letterAmount <= pw.max)
                return true;
            else
                return false;
        }
    }

    class Password
    {
        public short min;
        public short max;
        public char letter;
        public string password;
    }
}
