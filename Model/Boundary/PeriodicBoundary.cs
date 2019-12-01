using System;

namespace Model
{


    public class PeriodicBoundary : IBoundaryCondition
    {
        public string Name
        {
            get
            {
                return this.ToString();
            }
        }

        // AbsorbingBoundary always return null Cell 
        ///   y ->
        ///x |22|20|21|22|20|
        ///| |02|00|01|02|00|
        ///v |12|10|11|12|10|
        ///  |22|20|21|22|20|
        ///  |02|00|01|02|00|
        public Cell GetBoundaryNeighbour(CelluralSpace space, int x, int y, BoundaryDirection direction)
        {
            switch (direction)
            {
                case BoundaryDirection.N:  return space.GetCell( space.GetXLength() - 1, y);
                case BoundaryDirection.NE:
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
                case BoundaryDirection.SE:
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
                case BoundaryDirection.SW: 
                    {
                        if (x == space.GetYLength() - 1)
                        {
                            if (y == 0)
                            {
                                return space.GetCell(0, 2);//WTF??
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
                case BoundaryDirection.NW: 
                    {
                        if (y == 0)
                        {
                            if (x == 0)
                            {
                                return space.GetCell(2, 2);//WTF??
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
