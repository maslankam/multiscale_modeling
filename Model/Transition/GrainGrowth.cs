using System;
using System.Linq;
using Model.Microelements;

namespace Model.Transition
{

    public class GrainGrowth : ITransitionRule
    {
        public Microelement NextState(Cell cell, Cell[] neighbours){ 

            if (cell?.MicroelementMembership == null)
            {
                var groups = (from c in neighbours
                             where c?.MicroelementMembership?.Id != null && c.MicroelementMembership is Grain
                             group c by c.MicroelementMembership).ToArray();

                if (!groups.Any())
                {
                    return null;
                }
                else if (groups.Length > 1)
                {
                    //Check if groups has this same count
                    var top = (from g in groups
                              let maxPower = groups.Max(r => r.Count())
                              where g.Count() == maxPower
                              select g.Key).ToArray();

                    int topCount = top.Length;
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

    


