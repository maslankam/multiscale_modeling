using System;

namespace Model{
    
    public enum BoundaryDirection{
        N, NE, E, SE , S, SW, W, NW
    }

    public interface IBoundaryCondition 
    {
        Cell GetBoundaryNeighbour(CelluralSpace space, int x, int y, BoundaryDirection direction);
    }

    
}
