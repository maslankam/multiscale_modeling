using Xunit;
using System.Drawing;

using Model;
using Model.Transition;

namespace Test
{
    public class GrainGrowTest
    {
        [Fact]
        public void EmptyCellEmptyNeighbourhoodTest()
        {
            ITransitionRule transition = new GrainGrowthRule();
            IBoundaryCondition boundary = new AbsorbingBoundary();
            INeighbourhood neighbourhood = new VonNeumanNeighbourhood(boundary);

            CelluralSpace space = new CelluralSpace(3);

            Cell[] neighbours = neighbourhood.GetNeighbours(space, 1, 1);
            var element = transition.NextState(space.GetCell(1, 1), neighbours);

            Assert.Null(element);
        }

        
        [Fact]
        public void VioletCellEmptyNeighbourhoodTest()
        {
            ITransitionRule transition = new GrainGrowthRule();
            IBoundaryCondition boundary = new AbsorbingBoundary();
            INeighbourhood neighbourhood = new VonNeumanNeighbourhood(boundary);

            CelluralSpace space = new CelluralSpace(3);

            Grain expected = new Grain(11, 0, Color.Violet);
            space.SetCellMembership(expected, 1, 1);

            Cell[] neighbours = neighbourhood.GetNeighbours(space, 1, 1);
            var result = transition.NextState(space.GetCell(1, 1), neighbours);

            Assert.Same(expected, result);
        }
     
        [Fact]
        public void GreenNorthNeighbourTest()
        {
            ITransitionRule transition = new GrainGrowthRule();
            IBoundaryCondition boundary = new AbsorbingBoundary();
            INeighbourhood neighbourhood = new VonNeumanNeighbourhood(boundary);

            CelluralSpace space = new CelluralSpace(3);

            Grain expected = new Grain(12, 0, Color.Green);
            space.SetCellMembership(expected, 0, 1);

            Cell[] neighbours = neighbourhood.GetNeighbours(space, 1, 1);
            var result = transition.NextState(space.GetCell(1, 1), neighbours);

            Assert.Same(expected, result);
        }
        
        [Fact]
        public void GreenNorthGoldEastNeighbourTest()
        {
            ITransitionRule transition = new GrainGrowthRule();
            IBoundaryCondition boundary = new AbsorbingBoundary();
            INeighbourhood neighbourhood = new VonNeumanNeighbourhood(boundary);

            CelluralSpace space = new CelluralSpace(3);

            Grain expectedA = new Grain(01, 0, Color.Green);
            Grain expectedB = new Grain(12, 0, Color.Gold);
            space.SetCellMembership(expectedA, 1, 1);
            space.SetCellMembership(expectedB, 1, 2);

            Cell[] neighbours = neighbourhood.GetNeighbours(space, 1, 1);
            var result = transition.NextState(space.GetCell(1, 1), neighbours);

            bool isAGrain = result.Equals(expectedA);
            bool isBGrain = result.Equals(expectedB);

            Assert.True(isAGrain ^ isBGrain);
        }
        
        [Fact]
        public void GreenNorthGoldEastGoldSouthNeighbourTest()
        {
            ITransitionRule transition = new GrainGrowthRule();
            IBoundaryCondition boundary = new AbsorbingBoundary();
            INeighbourhood neighbourhood = new VonNeumanNeighbourhood(boundary);

            CelluralSpace space = new CelluralSpace(3);

            Grain expected = new Grain(12, 0, Color.Gold);
            Grain green = new Grain(1, 0, Color.Green);

            space.SetCellMembership(green, 0, 1);
            space.SetCellMembership(expected, 1, 1);
            space.SetCellMembership(expected, 2, 1);

            Cell[] neighbours = neighbourhood.GetNeighbours(space, 1, 1);
            var result = transition.NextState(space.GetCell(1, 1), neighbours);

            Assert.Same(expected, result);
        }
    }
}
