using System;
using System.Collections.Generic;

namespace Day_2_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("AdventOfCode - Day 2-2\n");

            Console.WriteLine("Enter path to textfile:");
            string path = Console.ReadLine();

            List<Password> passwords = new List<Password>();
            foreach (string line in System.IO.File.ReadAllLines(path))
            {
                passwords.Add(ParsePassword(line));
            }

            int validPasswords = 0;

            foreach (Password pw in passwords)
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

            string pos1 = chars[pointer].ToString();
            pointer++;
            if (chars[pointer] == '-')
                pointer++;
            else
            {
                pos1 += chars[pointer];
                pointer += 2;
            }
            newPassword.pos1 = short.Parse(pos1);
            newPassword.pos1--;

            string pos2 = chars[pointer].ToString();
            pointer++;
            if (chars[pointer] == ' ')
                pointer++;
            else
            {
                pos2 += chars[pointer];
                pointer += 2;
            }
            newPassword.pos2 = short.Parse(pos2);
            newPassword.pos2--;

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
            bool isValid = false;

            if (pw.password[pw.pos1] == pw.letter)
                isValid = !isValid;

            if (pw.password[pw.pos2] == pw.letter)
                isValid = !isValid;

            return isValid;
        }
    }

    class Password
    {
        public short pos1;
        public short pos2;
        public char letter;
        public string password;
    }
}
