using System;
using Xunit;
using System.Drawing;

using Model;
using System.Diagnostics;

namespace Test
{
    public class GrainGrowTest
    {
        [Fact]
        public void EmptyCellEmptyNeighbourhoodTest()
        {
            ITransitionRule transition = new GrainGrowthRule();
            IBoundaryCondition boundary = new AbsorbingBoundary();
            INeighbourhood neighbourhood = new VonNeumanNeighborhood(boundary);

            CelluralSpace space = new CelluralSpace(3);

            Cell[] neighbours = neighbourhood.GetNeighbours(space, 1, 1);
            var element = transition.NextState(space.GetCell(1, 1), neighbours);

            Assert.Null(element);
        }

        /*[Fact]
        public void NullArgumentExceptionsTest()
        {
            CleanUp();

            var ex = Assert.Throws<ArgumentNullException>(() => TransitionRule.NextState(null, 0, 0));
            Assert.Equal("Space cannot be null", ex.ParamName);
        }

        [Fact]
        public void SpaceSizeArgumentExceptionTest()
        {
            CleanUp();
            Cell[,] invalidSpace = new Cell[1, 1];

            var ex = Assert.Throws<ArgumentException>(() => TransitionRule.NextState(invalidSpace, 0, 0));
            Assert.Equal("Space size [1,1] is less than minimum [2,2]", ex.Message);

            invalidSpace = new Cell[1, 2];
            ex = Assert.Throws<ArgumentException>(() => TransitionRule.NextState(invalidSpace, 0, 0));
            Assert.Equal("Space size [1,2] is less than minimum [2,2]", ex.Message);

            invalidSpace = new Cell[2, 1];
            ex = Assert.Throws<ArgumentException>(() => TransitionRule.NextState(invalidSpace, 0, 0));
            Assert.Equal("Space size [2,1] is less than minimum [2,2]", ex.Message);
        }

        [Fact]
        public void ArgumentOutOfRangeExceptionTest()
        {
            CleanUp();
            PopulateCells();

            var ex = Assert.Throws<ArgumentOutOfRangeException>
                (() => TransitionRule.NextState(space, -1, 0));
            Assert.Equal($"-1,0 is out of space range [3,3]", ex.ParamName);

            ex = Assert.Throws<ArgumentOutOfRangeException>
               (() => TransitionRule.NextState(space, 0, -1));
            Assert.Equal($"0,-1 is out of space range [3,3]", ex.ParamName);

            ex = Assert.Throws<ArgumentOutOfRangeException>
               (() => TransitionRule.NextState(space, 3, 1));
            Assert.Equal($"3,1 is out of space range [3,3]", ex.ParamName);

            ex = Assert.Throws<ArgumentOutOfRangeException>
               (() => TransitionRule.NextState(space, 1, 3));
            Assert.Equal($"1,3 is out of space range [3,3]", ex.ParamName);

        }
        */
        [Fact]
        public void VioletCellEmptyNeighbourhoodTest()
        {
            ITransitionRule transition = new GrainGrowthRule();
            IBoundaryCondition boundary = new AbsorbingBoundary();
            INeighbourhood neighbourhood = new VonNeumanNeighborhood(boundary);

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
            INeighbourhood neighbourhood = new VonNeumanNeighborhood(boundary);

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
            INeighbourhood neighbourhood = new VonNeumanNeighborhood(boundary);

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
            INeighbourhood neighbourhood = new VonNeumanNeighborhood(boundary);

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
