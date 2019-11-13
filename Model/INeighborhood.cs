using System;
using System.Collections.Generic;

namespace Model{
    
    
    public interface INeighborhood //TODO: This code probably probably won't be use, to be deleted ! THere is static class instead
    {
        public IEnumerable<Cell> Neighbours(Cell[,] space, int x, int y, 
                    Func<Cell[,], int, int, BoundaryDirection, Tuple<int,int>> boundaryCondition);
    }
    
}
