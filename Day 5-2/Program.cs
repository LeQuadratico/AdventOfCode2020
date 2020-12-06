using System;
using System.Collections.Generic;

namespace Day_5_2
{
    class Program
    {
        public const short rows = 128;
        public const short columns = 8;

        static void Main(string[] args)
        {
            Console.WriteLine("AdventOfCode - Day 5-2\n");

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

            bool[,] usedSeats = new bool[Program.columns, Program.rows];
            foreach (Pass pass in passes)
            {
                usedSeats[pass.column, pass.row] = true;
            }

            Console.Write("\n");
            for (int x = 0; x < Program.columns; x++)
            {
                for (int y = 0; y < Program.rows; y++)
                {
                    if (usedSeats[x, y])
                        Console.Write("X");
                    else
                        Console.Write(".");
                }
                Console.Write("\n");
            }

            for (int x = 0; x < Program.columns; x++)
            {
                for (int y = 0; y < Program.rows; y++)
                {
                    if (y <= 6 || y >= 113)
                        continue;

                    if (!usedSeats[x, y])
                        Console.WriteLine("\nThere is a free seat at column " + x + ", row " + y + " with the id " + (y * 8 + x));
                }
            }
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
