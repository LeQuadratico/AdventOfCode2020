using System;
using System.Collections.Generic;

namespace Day_12_1
{
    class Program
    {
        static List<Instruction> instructions = new List<Instruction>();
        static void Main(string[] args)
        {
            Console.WriteLine("AdventOfCode - Day 12-1\n");

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
                    case 'R':
                        ins.type = InstructionType.ROTATE;
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

                if (ins.type != InstructionType.ROTATE)
                {
                    ins.amount = amount;
                }
                else
                {
                    //Rotate
                    if (line[0] == 'L')
                    {
                        //Rotate to left
                        ins.amount = -amount;
                    }
                    else
                    {
                        //Rotate to right
                        ins.amount = amount;
                    }
                }
                instructions.Add(ins);
            }

            //Starting facing east
            short rotation = 90;
            int posX = 0;
            int posY = 0;

            foreach (Instruction ins in instructions)
            {
                if (ins.type == InstructionType.MOVE)
                {
                    switch(ins.direction)
                    {
                        case Direction.NORTH:
                            posY += ins.amount;
                            break;
                        case Direction.EAST:
                            posX += ins.amount;
                            break;
                        case Direction.SOUTH:
                            posY -= ins.amount;
                            break;
                        case Direction.WEST:
                            posX -= ins.amount;
                            break;
                    }
                }
                else if (ins.type == InstructionType.ROTATE)
                {
                    rotation += (short)ins.amount;

                    if (rotation >= 360)
                        rotation -= 360;
                    else if (rotation < 0)
                        rotation += 360;
                }
                else
                {
                    //Forward
                    switch(rotation)
                    {
                        case 0:
                            posY += ins.amount;
                            break;
                        case 90:
                            posX += ins.amount;
                            break;
                        case 180:
                            posY -= ins.amount;
                            break;
                        case 270:
                            posX -= ins.amount;
                            break;
                    }
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
