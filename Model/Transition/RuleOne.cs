using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Model.Transition
{
    public class RuleOne : ITransitionRule
    {
        public Microelement NextState(Cell cell, Cell[] neighbours)
        {
            if (cell?.MicroelementMembership != null) return cell.MicroelementMembership;
            
            var twiCells = neighbours.Concat(neighbours).ToArray();

            int grainsStreak = 0;
            int streakId = -1;

            for (int i = 0 ; i < twiCells.Count(); i++)
            {
                if (twiCells[i]?.MicroelementMembership == null)
                {
                    grainsStreak = 0;
                    streakId = -1;
                    continue;
                }
                var element = twiCells[i].MicroelementMembership;

                if (!(element is Grain) )
                {
                    grainsStreak = 0;
                    streakId = -1;
                }
                else
                {
                    if (element.Id == streakId || streakId == -1)
                    {
                        grainsStreak++;
                        streakId = element.Id;

                        if (grainsStreak > 4) return element;
                    }
                    else
                    {
                        grainsStreak = 0;
                        streakId = -1;
                    }
                    
                }
            }

            return null;
        }
    }
}
