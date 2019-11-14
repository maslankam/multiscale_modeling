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
            Console.WriteLine("Hello World!");
            var automata = new CelluralAutomaton();
            var g0 = new Grain(0, Color.Red);
            var g1 = new Grain(1, Color.Blue);
            var g2 = new Grain(2, Color.Yellow);
            var g3 = new Grain(3, Color.Yellow);
            var g4 = new Grain(4, Color.Yellow);
            var g5 = new Grain(5, Color.Yellow);
            var g6 = new Grain(6, Color.Yellow);
            var g7 = new Grain(7, Color.Yellow);
            var g8 = new Grain(8, Color.Yellow);

            var space = automata.Space.currentState;
            space[0, 0].GrainMembership = g0;
            space[0, 1].GrainMembership = g1;
            space[0, 2].GrainMembership = g2;
            space[1, 0].GrainMembership = g1;
            space[1, 1].GrainMembership = g4;
            space[1, 2].GrainMembership = g5;
            space[2, 0].GrainMembership = g6;
            space[2, 1].GrainMembership = g5;
            space[2, 2].GrainMembership = g8;

            Cell[] neighbours = VonNeumanNeighborhood.Neighbours(
                    space, 1, 1, AbsorbingBoundary.BoundaryCondition); //TODO: More options (boundary and neighbourhood) !!




            foreach (Cell c in neighbours)
            {
                if (c == null)
                {
                    Console.WriteLine("Null Cell");
                }
                else
                {
                    if(c.GrainMembership == null)
                    {
                        Console.WriteLine("Null Membership");
                    }
                    else
                    {
                        if(c.GrainMembership.Id == null)
                        {
                            Console.WriteLine("Null Id");
                        }
                        else
                        {
                            Console.WriteLine(c.GrainMembership.Id.ToString());
                        }
                    }
                }
                
            }

            Console.WriteLine();
            Console.WriteLine("LINQ");

            var groups = from c in neighbours
                         where c?.GrainMembership?.Id != null // TODO: is it ok?
                         group c by c.GrainMembership;

            if(groups.Count() > 1)
            {
                //Check if groups has this same count
                var top = from g in groups
                          let maxPower = groups.Max(r => r.Count())
                          where g.Count() == maxPower
                          select new { g.Key };

                int topCount = top.Count();
                if (topCount > 1)
                {
                    //Take a random one
                    var r = new Random();
                    int randomIndex = r.Next(0, topCount - 1);
                    //return top.ElementAt(randomIndex);
                }
                else
                {
                    //return top.First
                }
            }
            else
            {
               //return groups.Key
            }


                /*neighbours.Where(c => c.GrainMembership.Id != null).
                                GroupBy(c => c.GrainMembership).
                                OrderBy(g => g.).
                                Select(g => g);*/

            /*
            // Find the strongest neighbour. In case of tie 
            var top = neighbours.Where(c => c.GrainMembership != null).
                                GroupBy(c => c.GrainMembership).
                                OrderBy(g => g.Count).
                                Select(g => g);
                                */


        }

    }
}
