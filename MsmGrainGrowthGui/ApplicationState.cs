using System;
using Model;

namespace GrainGrowthGui
{
    public class ApplicationState
    {
        public CelluralAutomaton automaton;
        public int spaceSize;
        public int grainsCount;
        public int inclusionsCount;
        public int minRadius;
        public int maxRadius;
        public ITransitionRule transition;
        public INeighbourhood neighbourhood;
        public IBoundaryCondition boundary;
        public bool isAutomatonGenerated;
        public bool isSaved;
    
        public ApplicationState(
                    CelluralAutomaton automaton,
                    int spaceSize,
                    int grainsCount,
                    int inclusionsCount,
                    int minRadius,
                    int maxRadius,
                    ITransitionRule transition,
                    INeighbourhood neighbourhood,
                    IBoundaryCondition boundary,
                    bool isGenerated,
                    bool isSaved
                    ){
                     this.automaton =  automaton;
                    this.spaceSize =  spaceSize;
                    this.grainsCount =  grainsCount;
                    this.inclusionsCount = inclusionsCount;
                    this.minRadius = minRadius;
                    this.maxRadius = maxRadius;
                    this.transition = transition;
                    this.neighbourhood =  neighbourhood;
                    this.boundary =  boundary;
                    this.isAutomatonGenerated =  isGenerated;
                    this.isSaved = isSaved;
                    }


        public static IBoundaryCondition GetBoundaryByName(string name)
        {
            switch(name)
            {
                case "Model.AbsorbingBoundary": 
                    return new AbsorbingBoundary();
                case "Model.PeriodicBoundary":
                    return new PeriodicBoundary();
                default: throw new ArgumentException();
            }
        }

        public static INeighbourhood GetNeighbourhoodByName(string name, IBoundaryCondition boundary)
        {
            switch(name)
            {
                case "Model.HexagonNeighbourhood":
                    return new HexagonNeighborhood(boundary);
                case "Model.MooreNeighbourhood":
                    return new MooreNeighbourhood(boundary);
                case "Model.PentagonNeighbourhood":
                    return new MooreNeighbourhood(boundary);
                case "Model.VonNeumanNeighbourhood":
                    return new VonNeumanNeighborhood(boundary);
                default: throw new ArgumentException();
            }
        }

        public static ITransitionRule GetTransitionByName(string name)
        {
            switch(name)
            {
                case "Model.GrainGrowthRule":
                    return new GrainGrowthRule();
                default: throw new ArgumentException();
            }
        }
    }

}
