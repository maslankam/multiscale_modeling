using System;
using System.Collections.Generic;
using System.Text;
namespace Model
{
    /// Moore checks N, E, S, W, NE, SE, SW, NW neighbours and return as Cell array
    /// |X|X|X|
    /// |X|c|X|
    /// |X|X|X|

    public class MooreNeighbourhood : INeighbourhood 
    {
        private IBoundaryCondition _boundary;

        public string Name
        {
            get { return this.ToString(); }
        }

        public MooreNeighbourhood(IBoundaryCondition boundary)
        {
            this._boundary = boundary;
        }

        public Cell[] GetNeighbours(CelluralSpace space, int x, int y)
        {
            Cell[] result = new Cell[8];
            var vonNeuman = new VonNeumanNeighbourhood(_boundary);
            Cell[] vonNeumanNeighbourhood = vonNeuman.GetNeighbours(space, x, y);
            result[0] = vonNeumanNeighbourhood[0];
            result[1] = vonNeumanNeighbourhood[1];
            result[2] = vonNeumanNeighbourhood[2];
            result[3] = vonNeumanNeighbourhood[3];

            //Check NE
            if ( x - 1 >= 0 && y + 1 < space.GetYLength())
            {
                result[4] = space.GetCell(x - 1, y + 1);
            }
            else
            {
                result[4] = _boundary.GetBoundaryNeighbour(space, x, y, BoundaryDirection.NE);
            }

            //Check SE neighbour
            if ( x + 1 < space.GetXLength() && y + 1 < space.GetYLength())
            {
                result[5] = space.GetCell(x + 1, y + 1);
            }
            else
            {
                result[5] = _boundary.GetBoundaryNeighbour(space, x, y, BoundaryDirection.SE);
            }

            //Check SW neighbour
            if (x + 1 < space.GetXLength() && y - 1 >= 0)
            {
                result[6] = space.GetCell(x + 1, y - 1);
            }
            else
            {
                result[6] = _boundary.GetBoundaryNeighbour(space, x, y, BoundaryDirection.SW);
            }

            //Check NW neighbour
            if (x - 1 >= 0 && y - 1 >= 0)
            {
                result[7] = space.GetCell(x - 1, y - 1);
            }
            else
            {
                result[7] = _boundary.GetBoundaryNeighbour(space, x, y, BoundaryDirection.NW);
            }

            return result;

        }
        public override string ToString()
        {
            return "MooreNeighbourhood";
        }
    }


}
