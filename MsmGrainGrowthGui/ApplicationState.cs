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