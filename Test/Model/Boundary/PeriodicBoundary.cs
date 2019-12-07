using Xunit;
using System.Drawing;

using Model;

namespace Test
{
    public class PeriodicBoundaryTest
    {
    


        [Fact]
        public void BoundaryTest()
        {
            IBoundaryCondition boundary = new PeriodicBoundary();

            CelluralSpace space = new CelluralSpace(3);

            int index = 0;
            foreach(var cell in space)
            {
                cell.MicroelementMembership = new Grain(index, index, Color.White);
                index++;
            }

            //x\y 0 1 2
            // 0 |a|b|c|
            // 1 |d|e|f|
            // 2 |g|h|i|

            //N boundary
            Assert.Same(space.GetCell(2,0), boundary.GetBoundaryNeighbour(space, 0, 0, BoundaryDirection.N));
            Assert.Same(space.GetCell(2,1), boundary.GetBoundaryNeighbour(space, 0, 1, BoundaryDirection.N));
            Assert.Same(space.GetCell(2,2), boundary.GetBoundaryNeighbour(space, 0, 2, BoundaryDirection.N));

            //NE
            Assert.Same(space.GetCell(2,1), boundary.GetBoundaryNeighbour(space, 0, 0, BoundaryDirection.Ne));
            Assert.Same(space.GetCell(2,2), boundary.GetBoundaryNeighbour(space, 0, 1, BoundaryDirection.Ne));
            Assert.Same(space.GetCell(2,0), boundary.GetBoundaryNeighbour(space, 0, 2, BoundaryDirection.Ne));
            Assert.Same(space.GetCell(0,0), boundary.GetBoundaryNeighbour(space, 1, 2, BoundaryDirection.Ne));
            Assert.Same(space.GetCell(1,0), boundary.GetBoundaryNeighbour(space, 2, 2, BoundaryDirection.Ne));

            //E
            Assert.Same(space.GetCell(0,0), boundary.GetBoundaryNeighbour(space, 0, 2, BoundaryDirection.E));
            Assert.Same(space.GetCell(1,0), boundary.GetBoundaryNeighbour(space, 1, 2, BoundaryDirection.E));
            Assert.Same(space.GetCell(2,0), boundary.GetBoundaryNeighbour(space, 2, 2, BoundaryDirection.E));

            //SE
            Assert.Same(space.GetCell(1,0), boundary.GetBoundaryNeighbour(space, 0, 2, BoundaryDirection.Se));
            Assert.Same(space.GetCell(2,0), boundary.GetBoundaryNeighbour(space, 1, 2, BoundaryDirection.Se));
            Assert.Same(space.GetCell(0,0), boundary.GetBoundaryNeighbour(space, 2, 2, BoundaryDirection.Se));
            Assert.Same(space.GetCell(0,2), boundary.GetBoundaryNeighbour(space, 2, 1, BoundaryDirection.Se));
            Assert.Same(space.GetCell(0,1), boundary.GetBoundaryNeighbour(space, 2, 0, BoundaryDirection.Se));

            //S
            Assert.Same(space.GetCell(0,2), boundary.GetBoundaryNeighbour(space, 2, 2, BoundaryDirection.S));
            Assert.Same(space.GetCell(0,1), boundary.GetBoundaryNeighbour(space, 2, 1, BoundaryDirection.S));
            Assert.Same(space.GetCell(0,0), boundary.GetBoundaryNeighbour(space, 2, 0, BoundaryDirection.S));

            //SW
            Assert.Same(space.GetCell(0,1), boundary.GetBoundaryNeighbour(space, 2, 2, BoundaryDirection.Sw));
            Assert.Same(space.GetCell(0,0), boundary.GetBoundaryNeighbour(space, 2, 1, BoundaryDirection.Sw));
            Assert.Same(space.GetCell(0,2), boundary.GetBoundaryNeighbour(space, 2, 0, BoundaryDirection.Sw));
            Assert.Same(space.GetCell(2,2), boundary.GetBoundaryNeighbour(space, 1, 0, BoundaryDirection.Sw));
            Assert.Same(space.GetCell(1,2), boundary.GetBoundaryNeighbour(space, 0, 0, BoundaryDirection.Sw));

            //W
            Assert.Same(space.GetCell(2,2), boundary.GetBoundaryNeighbour(space, 2, 0, BoundaryDirection.W));
            Assert.Same(space.GetCell(1,2), boundary.GetBoundaryNeighbour(space, 1, 0, BoundaryDirection.W));
            Assert.Same(space.GetCell(0,2), boundary.GetBoundaryNeighbour(space, 0, 0, BoundaryDirection.W));

            //NW
            Assert.Same(space.GetCell(1,2), boundary.GetBoundaryNeighbour(space, 2, 0, BoundaryDirection.Nw));
            Assert.Same(space.GetCell(0,2), boundary.GetBoundaryNeighbour(space, 1, 0, BoundaryDirection.Nw));
            Assert.Same(space.GetCell(2,2), boundary.GetBoundaryNeighbour(space, 0, 0, BoundaryDirection.Nw));
            Assert.Same(space.GetCell(2,0), boundary.GetBoundaryNeighbour(space, 0, 1, BoundaryDirection.Nw));
            Assert.Same(space.GetCell(2,1), boundary.GetBoundaryNeighbour(space, 0, 2, BoundaryDirection.Nw));

        }
    }
}