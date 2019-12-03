using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Model.Transition
{
    class RuleOne : ITransitionRule
    {
        public Microelement NextState(Cell cell, Cell[] neighbours)
        {
            
            neighbours.Concat(neighbours).ToArray();

            int grainsStreak = 0;
            int maxStreak = 0;
            Microelement strongestElement = null;

            for (int i = 1 ; i < neighbours.Count(); i++)
            {
                if (neighbours[i - 1]?.MicroelementMembership?.Id == null)
                {
                    grainsStreak = 0;
                    continue;
                }
                if (neighbours[i]?.MicroelementMembership is Grain && 
                    neighbours[i - 1]?.MicroelementMembership?.Id == neighbours[i]?.MicroelementMembership?.Id && 
                    neighbours[i - 1]?.MicroelementMembership?.Phase == neighbours[i]?.MicroelementMembership?.Phase)
                {
                    grainsStreak++;
                }
                else
                {
                    grainsStreak = 0;
                }

                if (grainsStreak > maxStreak)
                {
                    maxStreak = grainsStreak;
                    strongestElement = neighbours[i].MicroelementMembership;
                }

            }

            if (maxStreak < 5) return null; 
            return strongestElement;
        }
    }
}
