using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using Model;


namespace Model{
    
    public static class TransitionRule
    {
        public static Grain NextState(Cell[,] space, int x, int y)
        {
            ValidateArguments(space, x, y);

            if (space[x, y]?.GrainMembership == null)
            {

                Cell[] neighbours = VonNeumanNeighborhood.Neighbours(
                    space, x, y, AbsorbingBoundary.BoundaryCondition); //TODO: More options (boundary and neighbourhood) !!

                var groups = from c in neighbours
                             where c?.GrainMembership?.Id != null
                             group c by c.GrainMembership;

                if (groups.Count() == 0)
                {
                    return null;
                }
                else if (groups.Count() > 1)
                {
                    //Check if groups has this same count
                    var top = from g in groups
                              let maxPower = groups.Max(r => r.Count())
                              where g.Count() == maxPower
                              select g.Key;

                    int topCount = top.Count();
                    if (topCount > 1)
                    {
                        //Take a random one
                        var r = new Random();
                        int randomIndex = r.Next(0, topCount - 1);
                        return top.ElementAt(randomIndex);
                    }
                    else
                    {
                        //Return the strongest neighbour
                        return top.First();
                    }
                }
                else
                {
                    //return the only neighbour
                    return groups.First().Key;
                }
            }
            else
            {
                //return self
                return space[x, y].GrainMembership;
            }
        }

        private static void ValidateArguments(Cell[,] space, int x, int y)
        {
            if (space == null)
                throw new ArgumentNullException("Space cannot be null");

            if (space.GetLength(0) < 2 || space.GetLength(1) < 2)
                throw new ArgumentException($"Space size [{space.GetLength(0)}," +
                                    $"{space.GetLength(1)}] is less than minimum [2,2]");

            if (x >= space.GetLength(0) || y >= space.GetLength(1) || x < 0 || y < 0)
                throw new ArgumentOutOfRangeException($"{x},{y} is out of space range " +
                                    $"[{space.GetLength(0)},{space.GetLength(1)}]");
        }
    }

    

}
