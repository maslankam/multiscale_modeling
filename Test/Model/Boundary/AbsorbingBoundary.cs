using System.Drawing;
using Model;
using Model.Boundary;
using Model.Microelements;
using Xunit;

namespace Test.Model.Boundary
{
    public class AbsorbingBoundaryTest
    {


        [Fact]
        public void BoundaryTest()
        {
            IBoundaryCondition boundary = new AbsorbingBoundary();

            CelluralSpace space = new CelluralSpace(3);

            int index = 0;
            foreach(var cell in space)
            {
                cell.MicroelementMembership = new Grain(index, index, Color.White);
                index++;
            }
            
            //N boundary
            Assert.Null(boundary.GetBoundaryNeighbour(space, 0, 0, BoundaryDirection.N));
            Assert.Null(boundary.GetBoundaryNeighbour(space, 0, 1, BoundaryDirection.N));
            Assert.Null(boundary.GetBoundaryNeighbour(space, 0, 2, BoundaryDirection.N));

            //NE
            Assert.Null(boundary.GetBoundaryNeighbour(space, 0, 0, BoundaryDirection.Ne));
            Assert.Null(boundary.GetBoundaryNeighbour(space, 0, 1, BoundaryDirection.Ne));
            Assert.Null(boundary.GetBoundaryNeighbour(space, 0, 2, BoundaryDirection.Ne));
            Assert.Null(boundary.GetBoundaryNeighbour(space, 1, 2, BoundaryDirection.Ne));
            Assert.Null(boundary.GetBoundaryNeighbour(space, 2, 2, BoundaryDirection.Ne));

            //E
            Assert.Null(boundary.GetBoundaryNeighbour(space, 0, 2, BoundaryDirection.E));
            Assert.Null(boundary.GetBoundaryNeighbour(space, 1, 2, BoundaryDirection.E));
            Assert.Null(boundary.GetBoundaryNeighbour(space, 2, 2, BoundaryDirection.E));

            //SE
            Assert.Null(boundary.GetBoundaryNeighbour(space, 0, 2, BoundaryDirection.Se));
            Assert.Null(boundary.GetBoundaryNeighbour(space, 1, 2, BoundaryDirection.Se));
            Assert.Null(boundary.GetBoundaryNeighbour(space, 2, 2, BoundaryDirection.Se));
            Assert.Null(boundary.GetBoundaryNeighbour(space, 2, 1, BoundaryDirection.Se));
            Assert.Null(boundary.GetBoundaryNeighbour(space, 2, 0, BoundaryDirection.Se));

            //S
            Assert.Null(boundary.GetBoundaryNeighbour(space, 2, 0, BoundaryDirection.S));
            Assert.Null(boundary.GetBoundaryNeighbour(space, 2, 1, BoundaryDirection.S));
            Assert.Null(boundary.GetBoundaryNeighbour(space, 2, 2, BoundaryDirection.S));

            //SW
            Assert.Null(boundary.GetBoundaryNeighbour(space, 2, 2, BoundaryDirection.Sw));
            Assert.Null(boundary.GetBoundaryNeighbour(space, 2, 1, BoundaryDirection.Sw));
            Assert.Null(boundary.GetBoundaryNeighbour(space, 2, 0, BoundaryDirection.Sw));
            Assert.Null(boundary.GetBoundaryNeighbour(space, 1, 0, BoundaryDirection.Sw));
            Assert.Null(boundary.GetBoundaryNeighbour(space, 0, 0, BoundaryDirection.Sw));

            //W
            Assert.Null(boundary.GetBoundaryNeighbour(space, 2, 0, BoundaryDirection.W));
            Assert.Null(boundary.GetBoundaryNeighbour(space, 1, 0, BoundaryDirection.W));
            Assert.Null(boundary.GetBoundaryNeighbour(space, 0, 0, BoundaryDirection.W));

            //NW
            Assert.Null(boundary.GetBoundaryNeighbour(space, 2, 0, BoundaryDirection.Nw));
            Assert.Null(boundary.GetBoundaryNeighbour(space, 1, 0, BoundaryDirection.Nw));
            Assert.Null(boundary.GetBoundaryNeighbour(space, 0, 0, BoundaryDirection.Nw));
            Assert.Null(boundary.GetBoundaryNeighbour(space, 0, 1, BoundaryDirection.Nw));
            Assert.Null(boundary.GetBoundaryNeighbour(space, 0, 2, BoundaryDirection.Nw));

        }
    }
}