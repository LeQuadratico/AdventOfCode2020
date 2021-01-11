using System;
using System.Collections.Generic;

namespace Day_17_2
{
    class Program
    {
        static int4[] allMoves;

        static void Main(string[] args)
        {
            Console.WriteLine("AdventOfCode - Day 17-2\n");

            Console.WriteLine("Enter path to textfile:");
            string path = Console.ReadLine();
            Console.WriteLine();
            string[] lines = System.IO.File.ReadAllLines(path);

            const int cycles = 6;

            bool[,,,] map = new bool[lines[0].Length + 2, lines.Length + 2, 1 + 2, 1 + 2];

            for (int i = 0; i < lines.Length; i++)
            {
                byte pointer = 0;

                while (pointer < lines[i].Length)
                {
                    if (lines[i][pointer] == '#')
                        map[pointer + 1, i + 1, 0 + 1, 0 + 1] = true;

                    pointer++;
                }
            }

            allMoves = GetAllMoves();
            Console.WriteLine("Loaded Moves");

            for (int i = 0; i < cycles; i++)
            {
                Console.WriteLine("Cycle " + (i + 1));
                bool[,,,] newMap = new bool[map.GetLength(0) + 2, map.GetLength(1) + 2, map.GetLength(2) + 2, map.GetLength(3) + 2];
                for (int w = 0; w < map.GetLength(3); w++)
                {
                    for (int z = 0; z < map.GetLength(2); z++)
                    {
                        for (int y = 0; y < map.GetLength(1); y++)
                        {
                            for (int x = 0; x < map.GetLength(0); x++)
                            {
                                int neighbors = GetActiveNeighbors(new int4(x, y, z, w), map);

                                bool beActive;
                                if (map[x, y, z, w])
                                {
                                    if (neighbors == 2 || neighbors == 3)
                                    {
                                        //remain active
                                        beActive = true;
                                    }
                                    else
                                    {
                                        //become inactive
                                        beActive = false;
                                    }
                                }
                                else
                                {
                                    if (neighbors == 3)
                                    {
                                        //become active
                                        beActive = true;
                                    }
                                    else
                                    {
                                        //remain inactive
                                        beActive = false;
                                    }
                                }

                                newMap[x + 1, y + 1, z + 1, w + 1] = beActive;
                            }
                        }
                    }
                }
                map = newMap;
                //PrintMap(map);
            }

            Console.WriteLine("Counting");
            int result = CountActiveCubes(map);

            Console.WriteLine("\nThere are " + result + " active cubes");
        }

        static byte GetActiveNeighbors(int4 pos, bool[,,,] map)
        {
            byte result = 0;

            int4[] moves = allMoves;

            foreach (int4 move in moves)
            {
                int4 posCheck = new int4(pos.x + move.x, pos.y + move.y, pos.z + move.z, pos.w + move.w);
                if (posCheck.x < 0 || posCheck.y < 0 || posCheck.z < 0 || posCheck.w < 0 ||
                    posCheck.x >= map.GetLength(0) || posCheck.y >= map.GetLength(1) || posCheck.z >= map.GetLength(2) || posCheck.w >= map.GetLength(3))
                    continue;

                if (map[posCheck.x, posCheck.y, posCheck.z, posCheck.w])
                    result++;
            }

            return result;
        }

        static int CountActiveCubes(bool[,,,] map)
        {
            int result = 0;
            for (int w = 0; w < map.GetLength(3); w++)
            {
                for (int z = 0; z < map.GetLength(2); z++)
                {
                    for (int y = 0; y < map.GetLength(1); y++)
                    {
                        for (int x = 0; x < map.GetLength(0); x++)
                        {
                            if (map[x, y, z, w])
                                result++;
                        }
                    }
                }
            }
            return result;
        }

        static int4[] GetAllMoves()
        {
            HashSet<int4> list = new HashSet<int4>();

            // 2 = undetermined
            int4 start = new int4(2, 2, 2, 2);
            AddNextMoves(start);

            void AddNextMoves(int4 move)
            {
                if (move.x == 2)
                {
                    AddNextMoves(new int4(-1, move.y, move.z, move.w));
                    AddNextMoves(new int4(0, move.y, move.z, move.w));
                    AddNextMoves(new int4(1, move.y, move.z, move.w));
                }
                else if (move.y == 2)
                {
                    AddNextMoves(new int4(move.x, -1, move.z, move.w));
                    AddNextMoves(new int4(move.x, 0, move.z, move.w));
                    AddNextMoves(new int4(move.x, 1, move.z, move.w));
                }
                else if (move.z == 2)
                {
                    AddNextMoves(new int4(move.x, move.y, -1, move.w));
                    AddNextMoves(new int4(move.x, move.y, 0, move.w));
                    AddNextMoves(new int4(move.x, move.y, 1, move.w));
                }
                else if (move.w == 2)
                {
                    AddNextMoves(new int4(move.x, move.y, move.z, -1));
                    AddNextMoves(new int4(move.x, move.y, move.z, 0));
                    AddNextMoves(new int4(move.x, move.y, move.z, 1));
                }
                else
                {
                    list.Add(move);
                }
            }

            list.Remove(new int4(0, 0, 0, 0));
            int4[] result = new int4[list.Count];
            list.CopyTo(result);
            return result;
        }

        static void PrintMap(bool[,,,] map)
        {
            for (int w = 0; w < map.GetLength(3); w++)
            {
                Console.WriteLine("\n\nw: " + w);
                for (int z = 0; z < map.GetLength(2); z++)
                {
                    Console.WriteLine("\nz: " + z);
                    for (int y = 0; y < map.GetLength(1); y++)
                    {
                        for (int x = 0; x < map.GetLength(0); x++)
                        {
                            bool b = map[x, y, z, w];
                            if (b)
                                Console.Write("#");
                            else
                                Console.Write(".");
                        }
                        Console.WriteLine();
                    }
                }
            }
        }

        struct int4
        {
            public int x;
            public int y;
            public int z;
            public int w;

            public int4(int x, int y, int z, int w)
            {
                this.x = x;
                this.y = y;
                this.z = z;
                this.w = w;
            }
        }
    }
}
