using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using Model;


namespace Model{
    
    public class GrainGrowthRule : ITransitionRule
    {
        public Microelement NextState(Cell cell, Cell[] neighbours){ 

            if (cell?.MicroelementMembership == null)
            {
                var groups = from c in neighbours
                             where c?.MicroelementMembership?.Id != null && c?.MicroelementMembership is Grain
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

    

