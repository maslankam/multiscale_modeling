using System;
using System.Linq;
using Model.Microelements;

namespace Model.Transition
{
    public class InclusionGrowthRule : ITransitionRule
    {
        private readonly int _step;
        public InclusionGrowthRule(int step)
        {
            _step = step;
        }

        public Microelement NextState(Cell cell, Cell[] neighbours)
        {
            if (cell?.MicroelementMembership == null)
            {
                var inclusions = from c in neighbours
                                    where c?.MicroelementMembership?.Id != null
                                    && c.MicroelementMembership is Inclusion
                                 select (Inclusion)c.MicroelementMembership;   
                var groups = (from i in inclusions
                                where i.Radius > _step
                                group i by i.Id).ToArray();

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
                              select g).ToArray();

                    int topCount = top.Length;
                    if (topCount > 1)
                    {
                        //Take a random one
                        var r = new Random();
                        int randomIndex = r.Next(0, topCount - 1);
                        return top.ElementAt(randomIndex).ElementAt(0);
                    }
                    else
                    {
                        //Return the strongest neighbour
                         return groups.First().ElementAt(0);
                    }
                }
                else
                {
                    //return the only neighbour
                    return groups.First().ElementAt(0);
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
