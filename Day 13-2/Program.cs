using System;
using System.Collections.Generic;

namespace Day_13_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("AdventOfCode - Day 13-2\n");

            Console.WriteLine("Enter path to textfile:");
            string path = Console.ReadLine();
            Console.WriteLine();
            string[] lines = System.IO.File.ReadAllLines(path);

            List<Bus> busses = new List<Bus>();
            int pointer = 0;
            uint busPointer = 0;
            string busString = string.Empty;
            while (pointer < lines[1].Length)
            {
                if (lines[1][pointer] == ',')
                {
                    busses.Add(new Bus(uint.Parse(busString), busPointer));
                    busPointer++;
                    busString = string.Empty;
                    pointer++;
                    continue;
                }
                if (lines[1][pointer] == 'x')
                {
                    busPointer++;
                    pointer += 2;
                    continue;
                }

                busString += lines[1][pointer];
                pointer++;
                continue;
            }
            if (busString != null)
            {
                busses.Add(new Bus(uint.Parse(busString), busPointer));
            }


            int lastIncludedBus = 0;
            ulong stepsize = busses[0].driveTime;

            ulong startTime = 0;
            while (true)
            {
                if (busses[lastIncludedBus + 1].DrivesAt(startTime))
                {
                    stepsize *= busses[lastIncludedBus + 1].driveTime;
                    lastIncludedBus++;
                }
                if (lastIncludedBus == busses.Count - 1)
                {
                    //found it
                    Console.WriteLine("The first timestamp is " + startTime);
                    break;
                }
                else
                {
                    startTime += stepsize;
                }
            }
        }
    }

    class Bus
    {
        public uint driveTime;
        public uint departureDelay;

        public Bus(uint time, uint departureDelay)
        {
            this.driveTime = time;
            this.departureDelay = departureDelay;
        }

        public bool DrivesAt(ulong time)
        {
            if ((time + departureDelay) % driveTime == 0 || time == departureDelay)
                return true;

            return false;
        }
    }
}
