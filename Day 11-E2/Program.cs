using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Day_11_E2
{
    class Program
    {
        public static Place[,] map;

        static void Main(string[] args)
        {
            Console.WriteLine("AdventOfCode - Day 11-E2 - Print Images\n");

            Console.WriteLine("Enter path to textfile:");
            string path = Console.ReadLine();
            Console.WriteLine();
            string[] lines = System.IO.File.ReadAllLines(path);

            map = new Place[lines[0].Length, lines.Length];

            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[0].Length; x++)
                {
                    switch (lines[y][x])
                    {
                        case '.':
                            map[x, y] = new Place(PlaceState.FLOOR, x, y);
                            break;
                        case 'L':
                            map[x, y] = new Place(PlaceState.EMPTY, x, y);
                            break;
                        case '#':
                            map[x, y] = new Place(PlaceState.USED, x, y);
                            break;
                    }
                }
            }
            MapToPng(0);

            int i = 1;
            for (int v = 0; v < 100; v++)
            {
                bool changed = false;
                foreach (Place p in map)
                {
                    if (p.PrepareNextStep())
                        changed = true;
                }

                if (changed)
                {
                    foreach (Place p in map)
                    {
                        p.ApplyNextStep();
                    }

                    MapToPng(i);
                    i++;
                }
                else
                {
                    //Does not change anymore
                    break;
                }

            }
        }

        static void PrintMap()
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    char c = ' ';
                    switch (map[x, y].GetState())
                    {
                        case PlaceState.FLOOR:
                            c = '.';
                            break;
                        case PlaceState.EMPTY:
                            c = 'L';
                            break;
                        case PlaceState.USED:
                            c = '#';
                            break;
                    }

                    Console.Write(c.ToString());
                }
                Console.Write("\n");
            }
        }

        static void MapToPng(int i)
        {
            Bitmap b = new Bitmap(map.GetLength(0), map.GetLength(1));

            for (int y = 0; y < map.GetLength(1); y++)
            {
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    Color c = Color.White;
                    switch (map[x, y].GetState())
                    {
                        case PlaceState.FLOOR:
                            c = Color.DarkGray;
                            break;
                        case PlaceState.EMPTY:
                            c = Color.LightGray;
                            break;
                        case PlaceState.USED:
                            c = Color.Red;
                            break;
                    }

                    b.SetPixel(x, y, c);
                }
            }

            b.Save(i + ".png", ImageFormat.Png);
        }

        public class Place
        {
            PlaceState state;
            int x;
            int y;

            PlaceState nextStep;

            public Place(PlaceState state, int x, int y)
            {
                this.state = state;
                this.x = x;
                this.y = y;
            }

            public bool PrepareNextStep()
            {
                byte neighboursUsed = CalculateNeighbours();

                if (neighboursUsed == 0 && state == PlaceState.EMPTY)
                    nextStep = PlaceState.USED;
                else if (neighboursUsed >= 5 && state == PlaceState.USED)
                    nextStep = PlaceState.EMPTY;
                else
                    nextStep = state;


                if (nextStep != state)
                    return true;
                return false;
            }

            public void ApplyNextStep()
            {
                state = nextStep;
            }

            byte CalculateNeighbours()
            {
                int2[] moves =
                {
                    new int2(1, 0),
                    new int2(1, 1),
                    new int2(0, 1),
                    new int2(-1, 1),
                    new int2(-1, 0),
                    new int2(-1, -1),
                    new int2(0, -1),
                    new int2(1, -1)
                };

                byte counter = 0;
                foreach (int2 v in moves)
                {
                    int nextX = x + v.x;
                    int nextY = y + v.y;

                    while (true)
                    {
                        if (nextX < 0 || nextX >= map.GetLength(0) || nextY < 0 || nextY >= map.GetLength(1))
                            break;

                        PlaceState ps = map[nextX, nextY].state;
                        if (ps == PlaceState.USED)
                        {
                            counter++;
                            break;
                        }
                        else if (ps == PlaceState.EMPTY)
                        {
                            break;
                        }
                        else
                        {
                            nextX += v.x;
                            nextY += v.y;
                        }
                    }
                }

                return counter;
            }

            public PlaceState GetState()
            {
                return state;
            }
        }

        public enum PlaceState
        {
            FLOOR,
            EMPTY,
            USED
        }

        public struct int2
        {
            public int x;
            public int y;

            public int2(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }
    }
}
