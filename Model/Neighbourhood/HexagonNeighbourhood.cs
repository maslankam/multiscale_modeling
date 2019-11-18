using System;
using System.Collections.Generic;

namespace Model
{
    public class HexagonNeighborhood : INeighbourhood
    {
        private IBoundaryCondition _boundary;

        public HexagonNeighborhood(IBoundaryCondition boundary)
        {
            this._boundary = boundary;
        }

        public Cell[] GetNeighbours(CelluralSpace space, int x, int y)
        {
            //TODO: any abstraction for N,W,S,E,NW,NE,SW,SE ??? It apears meany times acros neighbourghood classes?? Abstract class ?
            // Nested if's are hard to read
            Cell[] result = new Cell[5];

            //Check N neighbour
            if (x - 1 >= 0)
            {
                result[0] = space.GetCell(x - 1, y);
            }
            else
            {
                result[0] = _boundary.GetBoundaryNeighbour(space, x, y, BoundaryDirection.W);
            }

            //Check S neighbour
            if (x + 1 <= space.GetYLength() - 1)
            {
                result[1] = space.GetCell(x + 1, y);
            }
            else
            {
                result[1] = _boundary.GetBoundaryNeighbour(space, x, y, BoundaryDirection.E);
            }


            var r = new Random();

            if(r.Next(0,1) == 0)
            {
                //right side

                //Check E
                if (y + 1 <= space.GetXLength() - 1)
                {
                    result[2] = space.GetCell(x, y + 1);
                }
                else
                {
                    result[2] = _boundary.GetBoundaryNeighbour(space, x, y, BoundaryDirection.S);
                }

                //Check NE
                if (x - 1 >= 0 && y + 1 < space.GetYLength())
                {
                    result[3] = space.GetCell(x - 1, y + 1);
                }
                else
                {
                    result[3] = _boundary.GetBoundaryNeighbour(space, x, y, BoundaryDirection.NE);
                }

                //Check SE neighbour
                if (x + 1 < space.GetXLength() && y + 1 < space.GetYLength())
                {
                    result[4] = space.GetCell(x + 1, y + 1);
                }
                else
                {
                    result[4] = _boundary.GetBoundaryNeighbour(space, x, y, BoundaryDirection.SE);
                }

            }
            else
            {
                //left side

                //Check W
                if (y - 1 >= 0)
                {
                    result[2] = space.GetCell(x, y - 1);
                }
                else
                {
                    result[2] = _boundary.GetBoundaryNeighbour(space, x, y, BoundaryDirection.N);
                }
                // Check SW neighbour
                if (x + 1 < space.GetXLength() && y - 1 >= 0)
                {
                    result[3] = space.GetCell(x + 1, y - 1);
                }
                else
                {
                    result[3] = _boundary.GetBoundaryNeighbour(space, x, y, BoundaryDirection.SW);
                }

                //Check NW neighbour
                if (x - 1 >= 0 && y - 1 >= 0)
                {
                    result[4] = space.GetCell(x - 1, y - 1);
                }
                else
                {
                    result[4] = _boundary.GetBoundaryNeighbour(space, x, y, BoundaryDirection.NW);
                }


            }

            return result;

        }
    }


}
