using System;
using System.Drawing;
using System.Linq;
using Model;


namespace Model{
    
    public static class TransitionRule
    {
        public static Grain NextState(Cell[,] space, int x, int y){
           
            if( space.GetLength(0) > 1 && space.GetLength(1) > 1) throw new ArgumentException();
            if (space == null) throw new ArgumentNullException();

            if (space[x, y].GrainMembership == null){
                
                Cell[] neighbours = VonNeumanNeighborhood.Neighbours(
                    space, x, y, AbsorbingBoundary.BoundaryCondition); //TODO: More options (boundary and neighbourhood) !!

                var groups = from c in neighbours
                             where c?.GrainMembership?.Id != null
                             group c by c.GrainMembership;

                if (groups.Count() == 0)
                {
                    //All neighbours are null, return self
                    return space[x, y].GrainMembership;
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
            else{
                //return self
                return space[x, y].GrainMembership;
            }
        }
        
    }

    

}
