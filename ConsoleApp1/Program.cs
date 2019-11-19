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
            IBoundaryCondition boundary = new AbsorbingBoundary();
            INeighbourhood neighbourhood = new VonNeumanNeighborhood(boundary);
            Automaton = new CelluralAutomaton(15, 0, 1, 2, 3, transition, neighbourhood, boundary);

            print();

            Automaton.NextStep();
            Console.WriteLine("###########");

            print();
        }

        private static void print()
        {
            for(int i = 0; i < Automaton.Space.GetXLength(); i++)
            {
                for(int j = 0; j < Automaton.Space.GetYLength(); j++)
                {
                    Console.Write(Automaton.Space.GetCell(i,j)?.Phase?.ToString() ?? "n");
                }
                Console.WriteLine();
            }


            foreach(var cell in Automaton.Space)
            {
               
            }
        }
    }
}
