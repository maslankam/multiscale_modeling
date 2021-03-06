﻿using System;
using Model.Boundary;

namespace Model.Neighbourhood
    {
        public class PentagonNeighbourhood : INeighbourhood
        {
            private readonly IBoundaryCondition _boundary;

        public string Name => this.ToString();

        public PentagonNeighbourhood(IBoundaryCondition boundary)
            {
                this._boundary = boundary;
            }

            public Cell[] GetNeighbours(CelluralSpace space, int x, int y)
            {

                var r = new Random();

                if (r.Next(0, 1) == 0)
                {
                    var hexagon = new HexagonNeighborhood(this._boundary);
                    return hexagon.GetNeighbours(space, x, y);
                }
                else
                {
                    Cell[] result = new Cell[5];
                    //Check E neighbour
                    if (y + 1 <= space.GetXLength() - 1)
                    {
                        result[0] = space.GetCell(x, y + 1);
                    }
                    else
                    {
                        result[0] = _boundary.GetBoundaryNeighbour(space, x, y, BoundaryDirection.E);
                    }

                    //Check W neighbour
                    if (y - 1 >= 0)
                    {
                        result[1] = space.GetCell(x, y - 1);
                    }
                    else
                    {
                        result[1] = _boundary.GetBoundaryNeighbour(space, x, y, BoundaryDirection.W);
                    }
                    if (r.Next(0, 1) == 0)
                    {
                        //Check N neighbour
                        if (x - 1 >= 0)
                        {
                            result[2] = space.GetCell(x - 1, y);
                        }
                        else
                        {
                            result[2] = _boundary.GetBoundaryNeighbour(space, x, y, BoundaryDirection.N);
                        }

                        //Check NW neighbour
                        if (x - 1 >= 0 && y - 1 >= 0)
                        {
                            result[3] = space.GetCell(x - 1, y - 1);
                        }
                        else
                        {
                            result[3] = _boundary.GetBoundaryNeighbour(space, x, y, BoundaryDirection.Nw);
                        }

                        //Check NE
                        if (x - 1 >= 0 && y + 1 < space.GetYLength())
                        {
                            result[4] = space.GetCell(x - 1, y + 1);
                        }
                        else
                        {
                            result[4] = _boundary.GetBoundaryNeighbour(space, x, y, BoundaryDirection.Ne);
                        }


                    }
                    else
                    {
                        //Check S neighbour
                        if (x + 1 <= space.GetYLength() - 1)
                        {
                            result[2] = space.GetCell(x + 1, y);
                        }
                        else
                        {
                            result[2] = _boundary.GetBoundaryNeighbour(space, x, y, BoundaryDirection.S);
                        }

                        //Check SE neighbour
                        if (x + 1 < space.GetXLength() && y + 1 < space.GetYLength())
                        {
                            result[3] = space.GetCell(x + 1, y + 1);
                        }
                        else
                        {
                            result[3] = _boundary.GetBoundaryNeighbour(space, x, y, BoundaryDirection.Se);
                        }


                        //Check SW neighbour
                        if (x + 1 < space.GetXLength() && y - 1 >= 0)
                        {
                            result[4] = space.GetCell(x + 1, y - 1);
                        }
                        else
                        {
                            result[4] = _boundary.GetBoundaryNeighbour(space, x, y, BoundaryDirection.Sw);
                        }

                    }
                    return result;
                }
           
            }
        public override string ToString()
        {
            return "PentagonNeighbourhood";
        }


    }

}

