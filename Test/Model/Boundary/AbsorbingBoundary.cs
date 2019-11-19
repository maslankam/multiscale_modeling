using System;
using Xunit;
using System.Drawing;

using Model;
using System.Diagnostics;

namespace Test
{
    public class AbsorbingBoundaryTest
    {

        [Fact]
        public void BoundaryTest()
        {
            IBoundaryCondition boundary = new AbsorbingBoundary();

            CelluralSpace space = new CelluralSpace(3);

            //N boundary
            Assert.Null(boundary.GetBoundaryNeighbour(space, 0, 0, BoundaryDirection.N));
            Assert.Null(boundary.GetBoundaryNeighbour(space, 0, 1, BoundaryDirection.N));
            Assert.Null(boundary.GetBoundaryNeighbour(space, 0, 2, BoundaryDirection.N));

            //NE
            Assert.Null(boundary.GetBoundaryNeighbour(space, 0, 0, BoundaryDirection.NE));
            Assert.Null(boundary.GetBoundaryNeighbour(space, 0, 1, BoundaryDirection.NE));
            Assert.Null(boundary.GetBoundaryNeighbour(space, 0, 2, BoundaryDirection.NE));
            Assert.Null(boundary.GetBoundaryNeighbour(space, 1, 2, BoundaryDirection.NE));
            Assert.Null(boundary.GetBoundaryNeighbour(space, 2, 2, BoundaryDirection.NE));

            //E
            Assert.Null(boundary.GetBoundaryNeighbour(space, 0, 2, BoundaryDirection.E));
            Assert.Null(boundary.GetBoundaryNeighbour(space, 1, 2, BoundaryDirection.E));
            Assert.Null(boundary.GetBoundaryNeighbour(space, 2, 2, BoundaryDirection.E));

            //SE
            Assert.Null(boundary.GetBoundaryNeighbour(space, 0, 2, BoundaryDirection.SE));
            Assert.Null(boundary.GetBoundaryNeighbour(space, 1, 2, BoundaryDirection.SE));
            Assert.Null(boundary.GetBoundaryNeighbour(space, 2, 2, BoundaryDirection.SE));
            Assert.Null(boundary.GetBoundaryNeighbour(space, 2, 1, BoundaryDirection.SE));
            Assert.Null(boundary.GetBoundaryNeighbour(space, 2, 0, BoundaryDirection.SE));

            //S
            Assert.Null(boundary.GetBoundaryNeighbour(space, 2, 0, BoundaryDirection.S));
            Assert.Null(boundary.GetBoundaryNeighbour(space, 2, 1, BoundaryDirection.S));
            Assert.Null(boundary.GetBoundaryNeighbour(space, 2, 2, BoundaryDirection.S));

            //SW
            Assert.Null(boundary.GetBoundaryNeighbour(space, 2, 2, BoundaryDirection.SW));
            Assert.Null(boundary.GetBoundaryNeighbour(space, 2, 1, BoundaryDirection.SW));
            Assert.Null(boundary.GetBoundaryNeighbour(space, 2, 0, BoundaryDirection.SW));
            Assert.Null(boundary.GetBoundaryNeighbour(space, 1, 0, BoundaryDirection.SW));
            Assert.Null(boundary.GetBoundaryNeighbour(space, 0, 0, BoundaryDirection.SW));

            //W
            Assert.Null(boundary.GetBoundaryNeighbour(space, 2, 0, BoundaryDirection.W));
            Assert.Null(boundary.GetBoundaryNeighbour(space, 1, 0, BoundaryDirection.W));
            Assert.Null(boundary.GetBoundaryNeighbour(space, 0, 0, BoundaryDirection.W));

            //NW
            Assert.Null(boundary.GetBoundaryNeighbour(space, 2, 0, BoundaryDirection.NW));
            Assert.Null(boundary.GetBoundaryNeighbour(space, 1, 0, BoundaryDirection.NW));
            Assert.Null(boundary.GetBoundaryNeighbour(space, 0, 0, BoundaryDirection.NW));
            Assert.Null(boundary.GetBoundaryNeighbour(space, 0, 1, BoundaryDirection.NW));
            Assert.Null(boundary.GetBoundaryNeighbour(space, 0, 2, BoundaryDirection.NW));

        }
    }
}