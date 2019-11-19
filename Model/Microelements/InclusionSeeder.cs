using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Linq;

namespace Model
{
    public class InclusionSeeder
    {
        private readonly IBoundaryCondition _boundary;

        public InclusionSeeder(IBoundaryCondition boundary){
            _boundary = boundary;
        }


        public void Seed(CelluralSpace space, List<Inclusion> inclusions){
            var r = new Random();
            foreach(var inclusion in inclusions){
                var xCenter = r.Next(0, space.GetXLength() - 1);
                var yCenter = r.Next(0, space.GetYLength() - 1);
                space.SetCellMembership(inclusion, xCenter, yCenter);
            }
            Grow(space);//TODO: grow 
        }

        private void Grow(CelluralSpace space){
            //In fact inclusions don't grow but to obtain round shape vonNeuman neighbourhood growing fits very weel,
            //This approach also resolves the problem with periodic boundary
            //But if inclusions appears next to each other and collide then they won't be perfect spherical. May apply radius checking for initializing??

            ITransitionRule transition;
            INeighbourhood neighbourhood = new VonNeumanNeighborhood(_boundary);

            for(int step = 0; step < Inclusion.MaxRadius; step++)
            {
                for (int i = 0; i < space.GetXLength(); i++)
                {
                    for (int j = 0; j < space.GetYLength(); j++)
                    {
                        Cell[] neighbours = neighbourhood.GetNeighbours(space, i, j);
                        transition = new InclusionGrowthRule(step);
                        var element = transition.NextState(space.GetCell(i,j), neighbours);
                        space.SetCellMembership(element, i, j);
                    }
                }
                step++;
            }

            
        }
        
        private class InclusionGrowthRule : ITransitionRule
        {
            private int _step;
                public InclusionGrowthRule(int step)
                {
                    _step = step;
                }

                public Microelement NextState(Cell cell, Cell[] neighbours){
                    if (cell?.MicroelementMembership == null)
                {
                var groups = from c in neighbours
                             where c?.MicroelementMembership?.Id != null 
                                    && c?.MicroelementMembership is Inclusion
                                    && (c.MicroelementMembership as Inclusion).Radius > _step
                             group c by c.MicroelementMembership;

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
                return cell.MicroelementMembership;
            }
                    
           }

        }
            

        }
}
