using System;
using System.Collections.Generic;

namespace Day_8_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("AdventOfCode - Day 8-2\n");

            Console.WriteLine("Enter path to textfile:");
            string path = Console.ReadLine();
            string[] lines = System.IO.File.ReadAllLines(path);

            List<Task> alltasks = new List<Task>();

            foreach (string line in lines)
            {
                Task task = new Task();

                string taskString = line[0].ToString() + line[1].ToString() + line[2].ToString();
                switch (taskString)
                {
                    case "acc":
                        task.type = TaskType.ACC;
                        break;
                    case "jmp":
                        task.type = TaskType.JMP;
                        break;
                    case "nop":
                        task.type = TaskType.NOP;
                        break;
                }

                short charPointer = 5;
                string numberString = String.Empty;
                while (charPointer < line.Length)
                {
                    numberString += line[charPointer];
                    charPointer++;
                }
                short number = short.Parse(numberString);
                if (line[4] == '-')
                    number *= -1;
                task.number = number;

                alltasks.Add(task);
            }

            int result = -1;
            foreach (Task oTask in alltasks)
            {
                Task task = new Task();

                if (oTask.type == TaskType.JMP)
                    task.type = TaskType.NOP;
                else if (oTask.type == TaskType.NOP)
                    task.type = TaskType.JMP;
                else
                    continue;

                task.number = oTask.number;
                List<Task> tempList = new List<Task>();
                tempList.AddRange(alltasks);

                //find and replace old task
                for (int i = 0; i < tempList.Count; i++)
                {
                    if(tempList[i] == oTask)
                    {
                        tempList[i] = task;
                        break;
                    }
                }

                bool[] alreadyRun = new bool[lines.Length];
                int accumulator = 0;
                int pointer = 0;

                while (true)
                {
                    if (pointer == tempList.Count)
                    {
                        result = accumulator;
                        break;
                    }

                    if (alreadyRun[pointer])
                        break;
                    else
                        alreadyRun[pointer] = true;

                    switch (tempList[pointer].type)
                    {
                        case TaskType.ACC:
                            accumulator += tempList[pointer].number;
                            pointer++;
                            break;
                        case TaskType.JMP:
                            pointer += tempList[pointer].number;
                            break;
                        case TaskType.NOP:
                            pointer++;
                            break;
                    }
                }

                if (result != -1)
                {
                    Console.WriteLine("\nThe accumulator is " + result);
                    break;
                }
            }


        }
    }

    class Task
    {
        public TaskType type;
        public int number;
    }

    enum TaskType
    {
        ACC,
        JMP,
        NOP
    }
}