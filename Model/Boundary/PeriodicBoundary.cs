using System;

namespace Model.Boundary
{


    public class PeriodicBoundary : IBoundaryCondition
    {
        public string Name => this.ToString();

        public Cell GetBoundaryNeighbour(CelluralSpace space, int x, int y, BoundaryDirection direction)
        {
            switch (direction)
            {
                case BoundaryDirection.N:  return space.GetCell( space.GetXLength() - 1, y);
                case BoundaryDirection.Ne:
                    {
                        if (y == space.GetYLength() - 1)
                        {
                            if(x == 0)
                            {
                                return space.GetCell(space.GetXLength() - 1, 0);
                            }
                            else
                            {
                                return space.GetCell(x - 1, 0);
                            }
                        }
                        else
                        {
                            return space.GetCell(space.GetXLength() - 1, y + 1);
                        }
                    }
                case BoundaryDirection.E:  return space.GetCell( x, 0);
                case BoundaryDirection.Se:
                    {
                        if (x == space.GetXLength() - 1)
                        {
                            if (y == space.GetYLength() - 1)
                            {
                                return space.GetCell(0, 0);
                            }
                            else
                            {
                                return space.GetCell(0, y + 1);
                            }
                        }
                        else
                        {
                            return space.GetCell(x + 1, 0);
                        }
                    }
                case BoundaryDirection.S:  return space.GetCell( 0, y);
                case BoundaryDirection.Sw: 
                    {
                        if (x == space.GetYLength() - 1)
                        {
                            if (y == 0)
                            {
                                return space.GetCell(0, space.GetYLength() - 1);
                            }
                            else
                            {
                                return space.GetCell(0, y - 1);
                            }
                        }
                        else
                        {
                            return space.GetCell(x + 1, space.GetYLength() - 1);
                        }
                    }
                case BoundaryDirection.W:  return space.GetCell( x, space.GetYLength() - 1);
                case BoundaryDirection.Nw: 
                    {
                        if (y == 0)
                        {
                            if (x == 0)
                            {
                                return space.GetCell(space.GetXLength() - 1, space.GetYLength() - 1);
                            }
                            else
                            {
                                return space.GetCell(x - 1, space.GetYLength() - 1);
                            }
                        }
                        else
                        {
                            return space.GetCell(space.GetXLength() - 1, y - 1);
                        }
                    }
                default: throw new ArgumentException("Wrong direction");
            }
        }

        public override string ToString()
        {
            return "PeriodicBoundary";
        }

    }


}
