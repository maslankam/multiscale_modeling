using System;

namespace Model
{
    public class HexagonNeighborhood : INeighbourhood
    {
        private IBoundaryCondition _boundary;

        public string Name
        {
            get { return this.ToString();}
        }

        public HexagonNeighborhood(IBoundaryCondition boundary)
        {
            this._boundary = boundary;
        }

        public Cell[] GetNeighbours(CelluralSpace space, int x, int y)
        {
            //TODO: any abstraction for N,W,S,E,NW,NE,SW,SE ??? It apears meany times acros neighbourghood classes?? Abstract class ?
            // Nested if's are hard to read
            Cell[] result = new Cell[6];
            var vonNeuman = new VonNeumanNeighbourhood(_boundary);
            Cell[] vonNeumanNeighbourhood = vonNeuman.GetNeighbours(space, x, y);
            result[0] = vonNeumanNeighbourhood[0];
            result[1] = vonNeumanNeighbourhood[1];
            result[2] = vonNeumanNeighbourhood[2];
            result[3] = vonNeumanNeighbourhood[3];

            var r = new Random();

            if(r.Next(0,100) > 50)
            {
                //Check NE
                if (x - 1 >= 0 && y + 1 < space.GetYLength())
                {
                    result[4] = space.GetCell(x - 1, y + 1);
                }
                else
                {
                    result[4] = _boundary.GetBoundaryNeighbour(space, x, y, BoundaryDirection.Ne);
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
            }
            else
            {
                //Check SE neighbour
                if (x + 1 < space.GetXLength() && y + 1 < space.GetYLength())
                {
                    result[4] = space.GetCell(x + 1, y + 1);
                }
                else
                {
                    result[4] = _boundary.GetBoundaryNeighbour(space, x, y, BoundaryDirection.Se);
                }
                //Check NW neighbour
                if (x - 1 >= 0 && y - 1 >= 0)
                {
                    result[5] = space.GetCell(x - 1, y - 1);
                }
                else
                {
                    result[5] = _boundary.GetBoundaryNeighbour(space, x, y, BoundaryDirection.Nw);
                }
            }
            return result;

        }
        public override string ToString()
        {
            return "HexagonNeighbourhood";
        }

    }


}
