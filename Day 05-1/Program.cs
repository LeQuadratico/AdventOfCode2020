using System;
using System.Collections.Generic;

namespace Day_05_1
{
    class Program
    {
        public const short rows = 128;
        public const short columns = 8;

        static void Main(string[] args)
        {
            Console.WriteLine("AdventOfCode - Day 5-1\n");

            Console.WriteLine("Enter path to textfile:");
            string path = Console.ReadLine();
            string[] lines = System.IO.File.ReadAllLines(path);

            List<Pass> passes = new List<Pass>();
            foreach (string line in lines)
            {
                Pass pass = new Pass();
                pass.encoded = line;
                pass.Decode();
                passes.Add(pass);
            }

            int highestId = 0;
            foreach(Pass pass in passes)
            {
                int id = pass.GetSeatID();
                if (id > highestId)
                    highestId = id;
            }

            Console.WriteLine("\nThe highest id is " + highestId);
        }
    }

    class Pass
    {
        public string encoded;
        public short row;
        public short column;

        public void Decode()
        {
            short minRow = 0;
            short maxRow = Program.rows - 1;

            for (int i = 0; i < 7; i++)
            {
                if (encoded[i] == 'B')
                {
                    //keep upper half
                    minRow = (short)(((maxRow - minRow) / 2f) + 0.5f + minRow);
                    //Console.WriteLine("B after " + minRow + "-" + maxRow);
                }
                else
                {
                    //keep lower half
                    maxRow = (short)(((maxRow - minRow) / 2f) - 0.5f + minRow);
                    //Console.WriteLine("F after " + minRow + "-" + maxRow);
                }
            }
            row = maxRow;

            short minColumn = 0;
            short maxColumn = Program.columns - 1;

            for (int i = 7; i < 10; i++)
            {
                if (encoded[i] == 'R')
                {
                    //keep upper half
                    minColumn = (short)(((maxColumn - minColumn) / 2f) + 0.5f + minColumn);
                    //Console.WriteLine("B after " + minRow + "-" + maxRow);
                }
                else
                {
                    //keep lower half
                    maxColumn = (short)(((maxColumn - minColumn) / 2f) - 0.5f + minColumn);
                    //Console.WriteLine("F after " + minRow + "-" + maxRow);
                }
            }
            column = maxColumn;
        }

        public int GetSeatID()
        {
            return row * 8 + column;
        }
    }
}
