using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Model.Transition
{
    class RuleTwo : ITransitionRule
    {
        public Microelement NextState(Cell cell, Cell[] neighbours)
        {
            Cell[] doubleNeighbours = (Cell[])neighbours.Concat(neighbours);

            int grainsStreak = 0;
            int maxStreak = 0;
            Microelement strongestElement = null;

            for (int i = 1; i < doubleNeighbours.Count(); i++)
            {
                if (doubleNeighbours[i]?.MicroelementMembership is Grain &&
                    doubleNeighbours[i - 1]?.MicroelementMembership.Id == doubleNeighbours[i]?.MicroelementMembership.Id &&
                    doubleNeighbours[i - 1]?.MicroelementMembership.Phase == doubleNeighbours[i]?.MicroelementMembership.Phase)
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
                    strongestElement = doubleNeighbours[i].MicroelementMembership;
                }

            }

            if (maxStreak < 5) return null;
            return strongestElement;
        }
    }
}