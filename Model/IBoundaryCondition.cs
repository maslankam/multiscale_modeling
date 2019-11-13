using System;

namespace Model{
    
    public enum BoundaryDirection{
        N, NE, E, SE , S, SW, W, NW
    }

    public interface IBoundaryCondition //TODO: This code probably probably won't be use, to be deleted ! THere is static class instead
    {
        public Tuple<int,int> BoundaryCondition(Cell[,] space, int x, int y, BoundaryDirection direction);
    }

    
}
