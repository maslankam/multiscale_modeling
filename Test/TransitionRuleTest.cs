using System;
using Xunit;
using System.Drawing;

using Model;

namespace Test
{
    public class TransitionRuleTest
    {
        private static Cell a, b, c, d, e, f, g, h, i;
        private static Cell[,] space;


        [Fact]
        public void EmptyCellEmptyNeighbourhoodTest()
        {
            ClearCells();

            Grain grain = TransitionRule.NextState(space, 1, 1);

            Assert.Null(grain);
        }

        /*public void NullSpaceArgumentTest()
        {
            ClearCells();
            try
            {
                Grain grain = TransitionRule.NextState(null, 1, 1);
                Assert.True(false);
            }
            catch(ArgumentNullException e)
            {
                Assert.True(true);
            }
            
        }*/

        public void VioletCellEmptyNeighbourhoodTest()
        {
            Grain expected = new Grain(11, Color.Violet);
            
            ClearCells();
            e = new Cell { GrainMembership = expected  };

            Grain result = TransitionRule.NextState(space, 1, 1);

            Assert.Same(expected, result);

        }

        public void GreenNorthNeighbourTest()
        {
            Grain expected = new Grain(11, Color.Red);

            ClearCells();
            e = new Cell { GrainMembership = new Grain(11, Color.Violet) };
            b = new Cell { GrainMembership = expected};

            Grain result = TransitionRule.NextState(space, 1, 1);

            Assert.Same(expected, result);
        }

        public void GreenNorthGoldEastNeighbourTest()
        {
            ClearCells();
            e = new Cell { GrainMembership = new Grain(11, Color.Violet) };
            b = new Cell { GrainMembership = new Grain(1, Color.Green) };
            f = new Cell { GrainMembership = new Grain(12, Color.Gold) };

            Grain result = TransitionRule.NextState(space, 1, 1);

            Assert.True(result.Equals(b) || result.Equals(f));
        }

        public void GreenNorthGoldEastGoldSouthNeighbourTest()
        {
            Grain expected = new Grain(12, Color.Gold);

            ClearCells();
            e = new Cell { GrainMembership = new Grain(11, Color.Violet) };
            b = new Cell { GrainMembership = new Grain(1, Color.Green) };
            f = new Cell { GrainMembership = expected };
            h = new Cell { GrainMembership = expected };

            Grain result = TransitionRule.NextState(space, 1, 1);

            Assert.Same(expected, result);
        }

        private static void ClearCells()
        {
            a = b = c = d = e = f = g = h = null;
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
            space = new Cell[,]{
                { a, b, c },
                { d, e, f },
                { g, h, i }
            };


        }
    }
}
