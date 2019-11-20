using System;
using Model;

namespace ConsoleApp1
{
    class Program
    {
        public static CelluralAutomaton Automaton;


        static void Main(string[] args)
        {
            ITransitionRule transition = new GrainGrowthRule();
            IBoundaryCondition boundary = new PeriodicBoundary();
            INeighbourhood neighbourhood = new VonNeumanNeighborhood(boundary);
            Automaton = new CelluralAutomaton(60, 4, 10, 1, 5, transition, neighbourhood, boundary);

            print();

            Automaton.NextStep();
            Console.WriteLine("###########");

            print();

            Automaton.NextStep();
            Console.WriteLine("###########");

            print();

            string input = "";
            while(input != "q"){
                Automaton.NextStep();
                Console.WriteLine("###########");

            print();
            input = Console.ReadLine();
            }


        }

        private static void print()
        {
            for(int i = 0; i < Automaton.Space.GetXLength(); i++)
            {
                for(int j = 0; j < Automaton.Space.GetYLength(); j++)
                {
                    if(Automaton.Space.GetCell(i,j)?.MicroelementMembership is Grain){
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        if(Automaton.Space.GetCell(i,j)?.MicroelementMembership is Inclusion)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                    }
                    
                    Console.Write(Automaton.Space.GetCell(i,j)?.MicroelementMembership?.Id.ToString() ?? "n");
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
            }
        }
    }
}
