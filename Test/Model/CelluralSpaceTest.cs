using System;
using Xunit;
using System.Drawing;

using Model;
using System.Diagnostics;

namespace Test
{
    public class CellueralSpaceTest
    {

        [Fact]
        public void InitializationTest()
        {
            CelluralSpace space = new CelluralSpace(3);

            Assert.Equal(3, space.Size);
            Assert.Equal(3, space.GetXLength());
            Assert.Equal(3, space.GetYLength());

            foreach (var cell in space)
            {
                Assert.Null(cell.MicroelementMembership);
                Assert.Null(cell.Phase);
            }
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(1)]
        public void InvalidArgumentInitializationTest(int size)
        {
            var ex = Assert.Throws<ArgumentException>(() => new CelluralSpace(size));
        }

        [Fact]
        public void GetSetCellTest()
        {
            CelluralSpace space = new CelluralSpace(3);

            Grain expectedGrain = new Grain(0, 0, Color.White);
            Cell cell = new Cell(expectedGrain);

            space.SetCellMembership(expectedGrain, 1, 1);
            var resultMicroelement = space.GetCell(1, 1).MicroelementMembership;

            Assert.Same(expectedGrain, resultMicroelement);
        }

        [Fact]
        public void CloneTest()
        {
            CelluralSpace expected = new CelluralSpace(3);

            CelluralSpace result = expected.Clone();

            for(int i = 0; i < 0; i++)
            {
                for(int j = 0; j < 0; j++)
                {
                    //Check membership reference same
                    Assert.Same(expected.GetCell(i, j).MicroelementMembership, result.GetCell(i, j).MicroelementMembership);
                    //Check if cell is a new object !! 
                    Assert.False(expected.GetCell(i, j).Equals(result.GetCell(i, j)));
                }
            }
        }
    }
}