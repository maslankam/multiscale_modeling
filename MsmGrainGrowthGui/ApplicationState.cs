using System;
using Model;
using Model.Boundary;
using Model.Executors;
using Model.Neighbourhood;
using Model.Transition;

namespace GrainGrowthGui
{
    public class ApplicationState
    {
        public readonly CellularAutomaton Automaton;
        public readonly int SpaceSize;
        public readonly int GrainsCount;
        public readonly int InclusionsCount;
        public readonly int MinRadius;
        public readonly int MaxRadius;
        public readonly ITransitionRule Transition;
        public readonly INeighbourhood Neighbourhood;
        public readonly IBoundaryCondition Boundary;
        public readonly bool IsAutomatonGenerated;
        public readonly bool IsSaved;
        public readonly ISimulationExecutor Executor;
    
        public ApplicationState(
                    CellularAutomaton automaton,
                    int spaceSize,
                    int grainsCount,
                    int inclusionsCount,
                    int minRadius,
                    int maxRadius,
                    ITransitionRule transition,
                    INeighbourhood neighbourhood,
                    IBoundaryCondition boundary,
                    bool isGenerated,
                    bool isSaved,
                    ISimulationExecutor executor
                    ){
                     this.Automaton =  automaton;
                    this.SpaceSize =  spaceSize;
                    this.GrainsCount =  grainsCount;
                    this.InclusionsCount = inclusionsCount;
                    this.MinRadius = minRadius;
                    this.MaxRadius = maxRadius;
                    this.Transition = transition;
                    this.Neighbourhood =  neighbourhood;
                    this.Boundary =  boundary;
                    this.IsAutomatonGenerated =  isGenerated;
                    this.IsSaved = isSaved;
                    this.Executor = executor;
        }


        public static IBoundaryCondition GetBoundaryByName(string name)
        {
            switch(name)
            {
                case "Model.Boundary.AbsorbingBoundary": 
                    return new AbsorbingBoundary();
                case "Model.Boundary.PeriodicBoundary":
                    return new PeriodicBoundary();
                default: throw new ArgumentException();
            }
        }

        public static INeighbourhood GetNeighbourhoodByName(string name, IBoundaryCondition boundary)
        {
            switch(name)
            {
                case "Model.Neighbourhood.HexagonNeighbourhood":
                    return new HexagonNeighborhood(boundary);
                case "Model.Neighbourhood.MooreNeighbourhood":
                    return new MooreNeighbourhood(boundary);
                case "Model.Neighbourhood.PentagonNeighbourhood":
                    return new PentagonNeighbourhood(boundary);
                case "Model.Neighbourhood.VonNeumanNeighbourhood":
                    return new VonNeumanNeighbourhood(boundary);
                default: throw new ArgumentException();
            }
        }

        public static ITransitionRule GetTransitionByName(string name)
        {
            switch(name)
            {
                case "Model.Transition.GrainGrowthRule":
                    return new GrainGrowthRule();
                default: throw new ArgumentException();
            }
        }

        public static ISimulationExecutor GetExecutorByName(string name, int step = 0)
        {
            switch (name)
            {
                case "Model.Executors.SimulationExecutor":

                    return new SimulationExecutor(step);
                case "Model.Executors.CurvatureExecutor":
                    return new CurvatureExecutor(step);

                default: throw new ArgumentException();
            }
        }

    }

}
