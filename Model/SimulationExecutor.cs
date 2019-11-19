using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace Model{
    public class SimulationExecutor
    {
        public int Step{get; private set;}

        public SimulationExecutor(){
        }

        public void NextState(CelluralSpace space, CelluralSpace lastSpace, ITransitionRule transition, INeighbourhood neighbourhood){
            for (int i = 0; i < space.GetXLength(); i++)
            {
                for (int j = 0; j < space.GetYLength(); j++)
                {
                    Cell[] neighbours = neighbourhood.GetNeighbours(lastSpace, i, j);
                    var element = transition.NextState(space.GetCell(i,j), neighbours);
                    space.SetCellMembership(element, i, j);
                }
            }
            Step++;
        }
        
        public void Reset()
        {
            Step = 0;
            throw new NotImplementedException();
        }
        
    }
}
