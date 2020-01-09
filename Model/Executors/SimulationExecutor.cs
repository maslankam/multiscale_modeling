using System;
using Model.Neighbourhood;
using Model.Transition;

namespace Model.Executors{
    public class SimulationExecutor : ISimulationExecutor
    {
        public string Name => ToString();

        private int Step{get; set;}

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

        public void NextState(CelluralSpace space, CelluralSpace lastSpace, ITransitionRule transition, INeighbourhood neighbourhood, int currentPhase){
            for (int i = 0; i < space.GetXLength(); i++)
            {
                for (int j = 0; j < space.GetYLength(); j++)
                {

                    Microelements.Microelement element;
                    var phase = lastSpace.GetCell(i,j)?.MicroelementMembership?.Phase ?? -2;
                    if(phase == currentPhase)
                    {
                        Cell[] neighbours = neighbourhood.GetNeighbours(lastSpace, i, j); //TODO: Storing neighbourhood in Cell object will increase memory consumption                                                   
                        element = transition.NextState(space.GetCell(i,j), neighbours); // but may increase performance
                        space.SetCellMembership(element, i, j);
                    }
                    else
                    {
                        element = lastSpace.GetCell(i,j).MicroelementMembership;
                    }
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
