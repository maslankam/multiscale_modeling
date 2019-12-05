using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace Model{
    public class SimulationExecutor : ISimulationExecutor
    {
        public string Name
        {
            get { return ToString(); }
        }

        public int Step{get; private set;}

        public SimulationExecutor(){
        }

        public SimulationExecutor(int step)
        {
            if (step < 0) throw new ArgumentException();
            Step = step;
        }

        public int ReturnStep()
        {
            return Step;
        }

        public void NextState(CelluralSpace space, CelluralSpace lastSpace, ITransitionRule transition, INeighbourhood neighbourhood){
            for (int i = 0; i < space.GetXLength(); i++)
            {
                for (int j = 0; j < space.GetYLength(); j++)
                {
                    Cell[] neighbours = neighbourhood.GetNeighbours(lastSpace, i, j); //TODO: Storing neighbourhood in Cell object will increase memory consumption                                                   
                    var element = transition.NextState(space.GetCell(i,j), neighbours); // but may increase performance
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

        public override string ToString()
        {
            return "SimulationExecutor";
        }
    }
}
