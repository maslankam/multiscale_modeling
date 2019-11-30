using System;
using System.Collections.Generic;

namespace Model{
    /// Von Neuman checks N, E, S, W neighbours and return as Cell array[N,E,S,W]
    /// |_|X|_|
    /// |X|c|X|
    /// |_|X|_|
    
    public class VonNeumanNeighbourhood : INeighbourhood
    {
        private IBoundaryCondition _boundary;


        public string Name
        {
            get { return this.ToString(); }
        }


        public VonNeumanNeighbourhood(IBoundaryCondition boundary){
            this._boundary = boundary;
        }


         public Cell[] GetNeighbours(CelluralSpace space, int x, int y){
             Cell[] result = new Cell[4];

            //Check N neighbour
            if (x - 1 >= 0)
            {
                result[0] = space.GetCell(x - 1, y);
            }
            else
            {
                result[0] = _boundary.GetBoundaryNeighbour(space, x, y, BoundaryDirection.W);
            }

            //Check E neighbour
            if (y + 1 <= space.GetXLength() - 1)
            {
                result[1] = space.GetCell(x, y + 1);
            }
            else
            {
                result[1] = _boundary.GetBoundaryNeighbour(space, x, y, BoundaryDirection.S);
            }

            //Check S neighbour
            if (x + 1 <= space.GetYLength() - 1)
            {
                result[2] = space.GetCell(x + 1, y);
            }
            else
            {
                result[2] = _boundary.GetBoundaryNeighbour(space, x, y, BoundaryDirection.E);
            }

            //Check W neighbour
            if (y - 1 >= 0)
            {
                result[3] = space.GetCell(x, y - 1);
            }
            else
            {
                result[3] = _boundary.GetBoundaryNeighbour(space, x, y, BoundaryDirection.N);
            }

            return result;

         }
        public override string ToString()
        {
            return "VonNeumanNeighbourhood";
        }
    }

    
}
