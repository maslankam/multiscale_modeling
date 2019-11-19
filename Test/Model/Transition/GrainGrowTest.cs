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
            Grain expected = new Grain(11, Color.Red);

            CleanUp();

            //e = new Cell { GrainMembership = new Grain(11, Color.Violet) };
            b = new Cell { GrainMembership = expected};
            InitializeSpace();

            Grain result = TransitionRule.NextState(space, 1, 1);

            Assert.Same(expected, result);

            CleanUp();
        }
        /*
        [Fact]
        public void GreenNorthGoldEastNeighbourTest()
        {
            CleanUp();

            //e = new Cell { GrainMembership = new Grain(11, Color.Violet) };
            b = new Cell { GrainMembership = new Grain(1, Color.Green) };
            f = new Cell { GrainMembership = new Grain(12, Color.Gold) };
            InitializeSpace();

            Grain result = TransitionRule.NextState(space, 1, 1);

            bool isBGrain = result.Equals(b.GrainMembership);
            bool isFGrain = result.Equals(f.GrainMembership);

            Assert.True(isBGrain || isFGrain);

            CleanUp();
        }

        [Fact]
        public void GreenNorthGoldEastGoldSouthNeighbourTest()
        {
            Grain expected = new Grain(12, Color.Gold);

            CleanUp();
           
            //e = new Cell { GrainMembership = new Grain(11, Color.Violet) };
            b = new Cell { GrainMembership = new Grain(1, Color.Green) };
            f = new Cell { GrainMembership = expected };
            h = new Cell { GrainMembership = expected };

            InitializeSpace();

            Grain result = TransitionRule.NextState(space, 1, 1);

            Assert.Same(expected, result);

            CleanUp();
        }

        private static void CleanUp()
        {
            a = b = c = d = e = f = g = h = i = null;
            space = null;
        }

        private static void PopulateCells()
        {
            a = new Cell { GrainMembership = new Grain(0, Color.Red) };
            b = new Cell { GrainMembership = new Grain(1, Color.Green) };
            c = new Cell { GrainMembership = new Grain(2, Color.Blue) };
            d = new Cell { GrainMembership = new Grain(10, Color.Yellow) };
            e = new Cell { GrainMembership = new Grain(11, Color.Violet) };
            f = new Cell { GrainMembership = new Grain(12, Color.Gold) };
            g = new Cell { GrainMembership = new Grain(20, Color.Coral) };
            h = new Cell { GrainMembership = new Grain(21, Color.Orange) };
            i = new Cell { GrainMembership = new Grain(22, Color.Azure) };
            InitializeSpace();
        }

        private static void InitializeSpace()
        {
            space = new Cell[,]{
                { a, b, c },
                { d, e, f },
                { g, h, i }
            };
        }*/
    }
}
