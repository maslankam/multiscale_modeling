using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace Model{
    public class SimulationExecutor
    {
        private CelluralSpace _space;
        public int Step{get; private set;}

        public SimulationExecutor(CelluralSpace space){
            this._space = space;
        }

        public void NextState(ITransitionRule transition, INeighbourhood neighbourhood, IBoundaryCondition boundary){
            for (int i = 0; i < this._space.GetLength(0); i++)
            {
                for (int j = 0; j < _space.GetLength(1); j++)
                {
                    Cell[] neighbours = neighbourhood.GetNeighbours(this, i, j, boundary);
                    _space[i,j].MicroelementMembership = transition.NextState(neighbourhood);
                }
            }
        }
        
        
    }
}
