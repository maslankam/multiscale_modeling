using System;

namespace Model
{


    public class PeriodicBoundary : IBoundaryCondition
    {
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
                case BoundaryDirection.NE: return space.GetCell(space.GetXLength() - 1, 0);
                case BoundaryDirection.E:  return space.GetCell( x, 0);
                case BoundaryDirection.SE: return space.GetCell( 0, 0);
                case BoundaryDirection.S:  return space.GetCell( 0, y);
                case BoundaryDirection.SW: return space.GetCell( 0, space.GetYLength() - 1);
                case BoundaryDirection.W:  return space.GetCell( x, space.GetYLength() - 1);
                case BoundaryDirection.NW: return space.GetCell( space.GetXLength(), space.GetYLength() );
                default: throw new ArgumentException("Wrong direction");
            }
        }
    }


}
