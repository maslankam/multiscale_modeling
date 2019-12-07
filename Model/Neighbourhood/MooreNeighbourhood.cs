using Model.Boundary;

namespace Model.Neighbourhood
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
            
            //Check N neighbour
            if (x - 1 >= 0)
            {
                result[0] = space.GetCell(x - 1, y);
            }
            else
            {
                result[0] = _boundary.GetBoundaryNeighbour(space, x, y, BoundaryDirection.N);
            }

            //Check NE
            if ( x - 1 >= 0 && y + 1 < space.GetYLength())
            {
                result[1] = space.GetCell(x - 1, y + 1);
            }
            else
            {
                result[1] = _boundary.GetBoundaryNeighbour(space, x, y, BoundaryDirection.Ne);
            }

            //Check E neighbour
            if (y + 1 <= space.GetXLength() - 1)
            {
                result[2] = space.GetCell(x, y + 1);
            }
            else
            {
                result[2] = _boundary.GetBoundaryNeighbour(space, x, y, BoundaryDirection.E);
            }

            //Check SE neighbour
            if ( x + 1 < space.GetXLength() && y + 1 < space.GetYLength())
            {
                result[3] = space.GetCell(x + 1, y + 1);
            }
            else
            {
                result[3] = _boundary.GetBoundaryNeighbour(space, x, y, BoundaryDirection.Se);
            }

            //Check S neighbour
            if (x + 1 <= space.GetYLength() - 1)
            {
                result[4] = space.GetCell(x + 1, y);
            }
            else
            {
                result[4] = _boundary.GetBoundaryNeighbour(space, x, y, BoundaryDirection.S);
            }

            //Check SW neighbour
            if (x + 1 < space.GetXLength() && y - 1 >= 0)
            {
                result[5] = space.GetCell(x + 1, y - 1);
            }
            else
            {
                result[5] = _boundary.GetBoundaryNeighbour(space, x, y, BoundaryDirection.Sw);
            }

            //Check W neighbour
            if (y - 1 >= 0)
            {
                result[6] = space.GetCell(x, y - 1);
            }
            else
            {
                result[6] = _boundary.GetBoundaryNeighbour(space, x, y, BoundaryDirection.W);
            }

            //Check NW neighbour
            if (x - 1 >= 0 && y - 1 >= 0)
            {
                result[7] = space.GetCell(x - 1, y - 1);
            }
            else
            {
                result[7] = _boundary.GetBoundaryNeighbour(space, x, y, BoundaryDirection.Nw);
            }

            return result;

        }
        public override string ToString()
        {
            return "MooreNeighbourhood";
        }
    }


}
