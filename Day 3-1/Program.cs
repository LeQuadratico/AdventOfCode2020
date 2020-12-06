using System;

namespace Day_3_1
{
    class Program
    {
        static bool[,] map;
        static void Main(string[] args)
        {
            Console.WriteLine("AdventOfCode - Day 3-1\n");

            Console.WriteLine("Enter path to textfile:");
            string path = Console.ReadLine();

            map = LoadStartMap(System.IO.File.ReadAllLines(path));

            short trees = SimulateDrive();

            Console.WriteLine("\nHe passed through " + trees + " trees.");
        }

        static bool[,] LoadStartMap(string[] lines)
        {
            bool[,] map = new bool[lines[0].Length, lines.Length];

            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    map[x, y] = ConvertCharToBool(lines[y][x]);
                }
            }

            return map;
        }

        static bool ConvertCharToBool(char c)
        {
            if (c == '.')
                return false;
            else
                return true;
        }

        static short SimulateDrive()
        {
            short trees = 0;

            int x = 0;
            int y = 0;

            while(true)
            {
                if (GetMapStatus(x, y))
                    trees++;

                x += 3;
                y += 1;

                if (y >= map.GetLength(1))
                {
                    return trees;
                }
            }
        }

        static bool GetMapStatus(int x, int y)
        {
            int over = x / map.GetLength(0);
            return map[x - (over * map.GetLength(0)), y];
        }
    }
}
