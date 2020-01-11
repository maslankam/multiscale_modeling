using System;
using Model.Neighbourhood;
using Model.Transition;
using System.Linq;

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
                
                    Cell[] neighbours = neighbourhood.GetNeighbours(lastSpace, i, j); //TODO: Storing neighbourhood in Cell object will increase memory consumption    


                    int neigboursSameCount = 0;

                    foreach(var n in neighbours)
                    {
                       bool isThisSame = Object.ReferenceEquals(n?.MicroelementMembership, lastSpace.GetCell(i, j)?.MicroelementMembership);
                        if (isThisSame)
                        {
                            neigboursSameCount++;
                        }
                    
                    }

                    if(neigboursSameCount == neighbours.Count())
                    {
                        space.GetCell(i, j).isBorder = false;
                    }
                    else
                    {
                        space.GetCell(i, j).isBorder = true;
                    }


                    var phaseNeighbours = (from n in neighbours
                                           where n?.MicroelementMembership?.Phase == currentPhase
                                           select n).ToArray();

                    element = transition.NextState(space.GetCell(i, j), phaseNeighbours); // but may increase performance

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
