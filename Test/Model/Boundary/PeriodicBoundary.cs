using System;
using Xunit;
using System.Drawing;

using Model;
using System.Diagnostics;

namespace Test
{
    public class PeriodicBoundaryTest
    {

        [Fact]
        public void BoundaryTest()
        {
            IBoundaryCondition boundary = new PeriodicBoundary();

            CelluralSpace space = new CelluralSpace(3);
            int i = 0;
            foreach(var cell in space)
            {
                cell.MicroelementMembership = new Grain(i, i, Color.White);
                i++;
            }

            //N boundary
            Assert.Same(space.GetCell(2,0), boundary.GetBoundaryNeighbour(space, 0, 0, BoundaryDirection.N));
            Assert.Same(space.GetCell(2,1), boundary.GetBoundaryNeighbour(space, 0, 1, BoundaryDirection.N));
            Assert.Same(space.GetCell(2,2), boundary.GetBoundaryNeighbour(space, 0, 2, BoundaryDirection.N));

            //NE
            Assert.Same(space.GetCell(2,1), boundary.GetBoundaryNeighbour(space, 0, 0, BoundaryDirection.NE));
            Assert.Same(space.GetCell(2,2), boundary.GetBoundaryNeighbour(space, 0, 1, BoundaryDirection.NE));
            Assert.Same(space.GetCell(2,0), boundary.GetBoundaryNeighbour(space, 0, 2, BoundaryDirection.NE));
            Assert.Same(space.GetCell(0,0), boundary.GetBoundaryNeighbour(space, 1, 2, BoundaryDirection.NE));
            Assert.Same(space.GetCell(1,0), boundary.GetBoundaryNeighbour(space, 2, 2, BoundaryDirection.NE));

            //E
            Assert.Same(space.GetCell(0,0), boundary.GetBoundaryNeighbour(space, 0, 2, BoundaryDirection.E));
            Assert.Same(space.GetCell(1,0), boundary.GetBoundaryNeighbour(space, 1, 2, BoundaryDirection.E));
            Assert.Same(space.GetCell(2,0), boundary.GetBoundaryNeighbour(space, 2, 2, BoundaryDirection.E));

            //SE
            Assert.Same(space.GetCell(1,0), boundary.GetBoundaryNeighbour(space, 0, 2, BoundaryDirection.SE));
            Assert.Same(space.GetCell(2,0), boundary.GetBoundaryNeighbour(space, 1, 2, BoundaryDirection.SE));
            Assert.Same(space.GetCell(0,0), boundary.GetBoundaryNeighbour(space, 2, 2, BoundaryDirection.SE));
            Assert.Same(space.GetCell(0,2), boundary.GetBoundaryNeighbour(space, 2, 1, BoundaryDirection.SE));
            Assert.Same(space.GetCell(0,1), boundary.GetBoundaryNeighbour(space, 2, 0, BoundaryDirection.SE));

            //S
            Assert.Same(space.GetCell(0,2), boundary.GetBoundaryNeighbour(space, 2, 2, BoundaryDirection.S));
            Assert.Same(space.GetCell(0,1), boundary.GetBoundaryNeighbour(space, 2, 1, BoundaryDirection.S));
            Assert.Same(space.GetCell(0,0), boundary.GetBoundaryNeighbour(space, 2, 0, BoundaryDirection.S));

            //SW
            Assert.Same(space.GetCell(0,1), boundary.GetBoundaryNeighbour(space, 2, 2, BoundaryDirection.SW));
            Assert.Same(space.GetCell(0,0), boundary.GetBoundaryNeighbour(space, 2, 1, BoundaryDirection.SW));
            Assert.Same(space.GetCell(0,2), boundary.GetBoundaryNeighbour(space, 2, 0, BoundaryDirection.SW));
            Assert.Same(space.GetCell(2,2), boundary.GetBoundaryNeighbour(space, 1, 0, BoundaryDirection.SW));
            Assert.Same(space.GetCell(1,2), boundary.GetBoundaryNeighbour(space, 0, 0, BoundaryDirection.SW));

            //W
            Assert.Same(space.GetCell(2,2), boundary.GetBoundaryNeighbour(space, 2, 0, BoundaryDirection.W));
            Assert.Same(space.GetCell(1,2), boundary.GetBoundaryNeighbour(space, 1, 0, BoundaryDirection.W));
            Assert.Same(space.GetCell(0,2), boundary.GetBoundaryNeighbour(space, 0, 0, BoundaryDirection.W));

            //NW
            Assert.Same(space.GetCell(1,2), boundary.GetBoundaryNeighbour(space, 2, 0, BoundaryDirection.NW));
            Assert.Same(space.GetCell(0,2), boundary.GetBoundaryNeighbour(space, 1, 0, BoundaryDirection.NW));
            Assert.Same(space.GetCell(2,2), boundary.GetBoundaryNeighbour(space, 0, 0, BoundaryDirection.NW));
            Assert.Same(space.GetCell(2,0), boundary.GetBoundaryNeighbour(space, 0, 1, BoundaryDirection.NW));
            Assert.Same(space.GetCell(2,1), boundary.GetBoundaryNeighbour(space, 0, 2, BoundaryDirection.NW));

        }
    }
}