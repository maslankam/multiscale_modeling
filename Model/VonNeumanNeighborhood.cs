using System;
using System.Collections.Generic;

namespace Model{
    /// Von Neuman checks N, E, S, W neighbours and return as Cell array[N,E,S,W]
    /// |_|X|_|
    /// |X|c|X|
    /// |_|X|_|
    
    public static class VonNeumanNeighborhood
    {
        public static Cell[] Neighbours(Cell[,] space, int x, int y, 
                    Func<Cell[,], int, int, BoundaryDirection, Tuple<int,int>> boundaryCondition){
                    
            Cell[] result = new Cell[4];

            //Check N neighbour
            if(y - 1 >= 0){
                result[0] = space[x, y - 1];
            }
            else{
                result[0] = AssignBoundaryCell(space, x, y, BoundaryDirection.N, boundaryCondition);
            }

            //Check E neighbour
            if(x + 1 <= space.GetLength(0) - 1){
                result[1] = space[x + 1, y];
            }
            else{
                result[1] = AssignBoundaryCell(space, x, y, BoundaryDirection.E, boundaryCondition);
            }

            //Check S neighbour
            if(y + 1 <= space.GetLength(0) - 1){
                result[2] = space[x, y + 1];
            }
            else{
                result[2] = AssignBoundaryCell(space, x, y, BoundaryDirection.S, boundaryCondition);
            }

            //Check W neighbour
            if(x - 1 >= 0){
                result[3] = space[x - 1, y];
            }
            else{
                result[3] = AssignBoundaryCell(space, x, y, BoundaryDirection.W, boundaryCondition);
            }

            return result;
        }

        private static Cell AssignBoundaryCell(Cell[,] space, int x, int y, BoundaryDirection direction, 
            Func<Cell[,], int, int, BoundaryDirection, Tuple<int,int>> boundaryCondition){
            var xy = boundaryCondition(space, x, y, direction);
                if(xy == null){
                    return null;
                }
                else{
                    return space[xy.Item1, xy.Item2];
                }
        }

    }

    
}
