using System;
using System.Collections.Generic;

namespace Day_12_2
{
    class Program
    {
        static List<Instruction> instructions = new List<Instruction>();
        static void Main(string[] args)
        {
            Console.WriteLine("AdventOfCode - Day 12-2\n");

            Console.WriteLine("Enter path to textfile:");
            string path = Console.ReadLine();
            Console.WriteLine();
            string[] lines = System.IO.File.ReadAllLines(path);

            foreach (string line in lines)
            {
                Instruction ins = new Instruction();

                switch (line[0])
                {
                    case 'N':
                        ins.type = InstructionType.MOVE;
                        ins.direction = Direction.NORTH;
                        break;
                    case 'S':
                        ins.type = InstructionType.MOVE;
                        ins.direction = Direction.SOUTH;
                        break;
                    case 'E':
                        ins.type = InstructionType.MOVE;
                        ins.direction = Direction.EAST;
                        break;
                    case 'W':
                        ins.type = InstructionType.MOVE;
                        ins.direction = Direction.WEST;
                        break;
                    case 'L':
                        ins.type = InstructionType.ROTATE;
                        break;
                    case 'R':
                        ins.type = InstructionType.ROTATE;
                        ins.rotateRight = true;
                        break;
                    case 'F':
                        ins.type = InstructionType.FORWARD;
                        break;
                }

                string amountString = String.Empty;
                byte pointer = 1;
                while (pointer < line.Length)
                {
                    amountString += line[pointer];
                    pointer++;
                }

                int amount = int.Parse(amountString);
                ins.amount = amount;
                instructions.Add(ins);
            }

            int posX = 0;
            int posY = 0;
            int wPosX = 10;
            int wPosY = 1;

            foreach (Instruction ins in instructions)
            {
                if (ins.type == InstructionType.MOVE)
                {
                    switch (ins.direction)
                    {
                        case Direction.NORTH:
                            wPosY += ins.amount;
                            break;
                        case Direction.EAST:
                            wPosX += ins.amount;
                            break;
                        case Direction.SOUTH:
                            wPosY -= ins.amount;
                            break;
                        case Direction.WEST:
                            wPosX -= ins.amount;
                            break;
                    }
                }
                else if (ins.type == InstructionType.ROTATE)
                {
                    if (ins.amount == 180)
                    {
                        wPosX = -wPosX;
                        wPosY = -wPosY;
                    }
                    else
                    {
                        if (ins.rotateRight)
                        {
                            if (ins.amount == 90)
                            {
                                int tPosX = wPosX;
                                wPosX = wPosY;
                                wPosY = -tPosX;
                            }
                            else if (ins.amount == 270)
                            {
                                int tPosX = wPosX;
                                wPosX = -wPosY;
                                wPosY = tPosX;
                            }
                        }
                        else
                        {
                            if (ins.amount == 90)
                            {
                                int tPosX = wPosX;
                                wPosX = -wPosY;
                                wPosY = tPosX;
                            }
                            else if (ins.amount == 270)
                            {
                                int tPosX = wPosX;
                                wPosX = wPosY;
                                wPosY = -tPosX;
                            }
                        }
                    }
                }
                else
                {
                    //Forward
                    posX += wPosX * ins.amount;
                    posY += wPosY * ins.amount;
                }
            }


            int result = Math.Abs(posX) + Math.Abs(posY);

            Console.WriteLine("The result is " + result);
        }
    }

    class Instruction
    {
        public int amount;
        public InstructionType type;
        public Direction direction;
        public bool rotateRight;
    }

    enum InstructionType
    {
        MOVE,
        ROTATE,
        FORWARD
    }

    enum Direction
    {
        NORTH,
        EAST,
        SOUTH,
        WEST
    }
}
