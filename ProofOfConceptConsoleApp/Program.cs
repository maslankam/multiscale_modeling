using System;

using Model;
using System.Drawing;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            CelluralAutomaton automaton = new CelluralAutomaton();

            Grain redGrain =  new Grain(0, Color.Red);
            Grain blueGrain = new Grain(1, Color.Blue);
            Grain cyanGrain = new Grain(2, Color.Cyan);
            Grain magentaGrain = new Grain(3, Color.Magenta);
            Grain yellowGrain = new Grain(4, Color.Yellow);
            Grain greenGrain = new Grain(5, Color.Green);
            Grain darkGreenGrain = new Grain(6, Color.Green);

            var space = automaton.Space.currentState;
            var previousSpace = automaton.Space.lastState;

            space[3, 4].GrainMembership = redGrain;
            space[26, 8].GrainMembership = blueGrain;
            space[0, 29].GrainMembership = cyanGrain;
            space[13, 19].GrainMembership = magentaGrain;
            space[26, 21].GrainMembership = yellowGrain;
            space[28, 14].GrainMembership = greenGrain;
            space[12, 12].GrainMembership = darkGreenGrain;


            string input = "";
            int step = 0;

            while (input != "q")
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"\nSTEP {step}\n####################################\n");

                for (int i = 0; i < space.GetLength(0); i++)
                {
                    for (int j = 0; j < space.GetLength(1); j++)
                    {
                        previousSpace[i, j] = space[i, j];
                    }
                }

                space = new Cell[30,30];
                for (int i = 0; i < space.GetLength(0); i++)
                {
                    for (int j = 0; j < space.GetLength(1); j++)
                    {
                        space[i, j] = new Cell();
                    }
                }


                for (int i = 0; i < space.GetLength(0); i++)
                    {
                        for (int j = 0; j < space.GetLength(1); j++)
                        {
                            if (step == 0)
                            {
                                space[i, j].GrainMembership = previousSpace[i, j].GrainMembership;
                            }
                            else
                            {
                                space[i, j].GrainMembership = TransitionRule.NextState(previousSpace, i, j);
                            }

                            string cellNumber = space?[i, j]?.GrainMembership?.Id?.ToString() ?? "n";
                            switch (cellNumber)
                            {
                                case "n": Console.ForegroundColor = ConsoleColor.Gray; break;
                                case "0": Console.ForegroundColor = ConsoleColor.Red; break;
                                case "1": Console.ForegroundColor = ConsoleColor.Blue; break;
                                case "2": Console.ForegroundColor = ConsoleColor.Cyan; break;
                                case "3": Console.ForegroundColor = ConsoleColor.Magenta; break;
                                case "4": Console.ForegroundColor = ConsoleColor.Yellow; break;
                                case "5": Console.ForegroundColor = ConsoleColor.Green; break;
                                case "6": Console.ForegroundColor = ConsoleColor.Gray; break;
                            default: Console.ForegroundColor = ConsoleColor.White; break;
                            }
                            Console.Write(cellNumber);
                            

                        }
                        Console.Write("\n");
                    }
                
                
                

                input = Console.ReadLine().ToLower();
                step++;
            }
        }
    }
}
