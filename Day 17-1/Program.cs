using System;

namespace Day_17_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("AdventOfCode - Day 17-1\n");

            Console.WriteLine("Enter path to textfile:");
            string path = Console.ReadLine();
            Console.WriteLine();
            string[] lines = System.IO.File.ReadAllLines(path);

            const int cycles = 6;

            bool[,,] map = new bool[lines[0].Length + 2, lines.Length + 2, 1 + 2];

            for (int i = 0; i < lines.Length; i++)
            {
                byte pointer = 0;

                while (pointer < lines[i].Length)
                {
                    if (lines[i][pointer] == '#')
                        map[pointer + 1, i + 1, 0 + 1] = true;

                    pointer++;
                }
            }

            //PrintMap(map);

            for (int i = 0; i < cycles; i++)
            {
                //Console.WriteLine("\n\nCycle " + (i + 1));
                bool[,,] newMap = new bool[map.GetLength(0) + 2, map.GetLength(1) + 2, map.GetLength(2) + 2];
                for (int z = 0; z < map.GetLength(2); z++)
                {
                    for (int y = 0; y < map.GetLength(1); y++)
                    {
                        for (int x = 0; x < map.GetLength(0); x++)
                        {
                            int neighbors = GetActiveNeighbors(new int3(x, y, z), map);

                            bool beActive;
                            if (map[x, y, z])
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

                            newMap[x + 1, y + 1, z + 1] = beActive;
                        }
                    }
                }
                map = newMap;
                //PrintMap(map);
            }

            int result = CountActiveCubes(map);

            Console.WriteLine("\nThere are " + result + " active cubes");
        }

        static byte GetActiveNeighbors(int3 pos, bool[,,] map)
        {
            byte result = 0;

            int3[] moves = new int3[]
            {
                new int3(0, 0, 1),
                new int3(0, 1, 0),
                new int3(0, 1, 1),
                new int3(1, 0, 0),
                new int3(1, 0, 1),
                new int3(1, 1, 0),
                new int3(1, 1, 1),

                new int3(0, 0, -1),
                new int3(0, -1, 0),
                new int3(0, -1, -1),
                new int3(-1, 0, 0),
                new int3(-1, 0, -1),
                new int3(-1, -1, 0),
                new int3(-1, -1, -1),

                new int3(0, -1, 1),
                new int3(1, -1, 0),
                new int3(1, -1, 1),

                new int3(0, 1, -1),
                new int3(1, 0, -1),
                new int3(1, 1, -1),

                new int3(-1, 0, 1),
                new int3(-1, 1, 0),
                new int3(-1, 1, 1),

                new int3(-1, -1, 1),
                new int3(-1, 1, -1),
                new int3(1, -1, -1)
            };

            foreach (int3 move in moves)
            {
                int3 posCheck = new int3(pos.x + move.x, pos.y + move.y, pos.z + move.z);
                if (posCheck.x < 0 || posCheck.y < 0 || posCheck.z < 0 ||
                    posCheck.x >= map.GetLength(0) || posCheck.y >= map.GetLength(1) || posCheck.z >= map.GetLength(2))
                    continue;

                if (map[posCheck.x, posCheck.y, posCheck.z])
                    result++;
            }

            return result;
        }

        static int CountActiveCubes(bool[,,] map)
        {
            int result = 0;
            for (int z = 0; z < map.GetLength(2); z++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    for (int x = 0; x < map.GetLength(0); x++)
                    {
                        if (map[x, y, z])
                            result++;
                    }
                }
            }
            return result;
        }

        static void PrintMap(bool[,,] map)
        {
            for (int z = 0; z < map.GetLength(2); z++)
            {
                Console.WriteLine("\nz: " + z);
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    for (int x = 0; x < map.GetLength(0); x++)
                    {
                        bool b = map[x, y, z];
                        if (b)
                            Console.Write("#");
                        else
                            Console.Write(".");
                    }
                    Console.WriteLine();
                }
            }
        }

        struct int3
        {
            public int x;
            public int y;
            public int z;

            public int3(int x, int y, int z)
            {
                this.x = x;
                this.y = y;
                this.z = z;
            }
        }
    }
}
