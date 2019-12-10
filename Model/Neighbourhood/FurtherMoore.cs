using Model.Boundary;

namespace Model.Neighbourhood{
    /// Further Moore checks NE, SE, SW, NW neighbours and return as Cell[]
    /// |X|_|X|
    /// |_|c|_|
    /// |X|_|X|
    
    public class FurtherMoore : INeighbourhood
    {
        private readonly IBoundaryCondition _boundary;


        public string Name => this.ToString();


        public FurtherMoore(IBoundaryCondition boundary){
            this._boundary = boundary;
        }


         public Cell[] GetNeighbours(CelluralSpace space, int x, int y){
             Cell[] result = new Cell[4];

            //Check NE
            if ( x - 1 >= 0 && y + 1 < space.GetYLength())
            {
                result[0] = space.GetCell(x - 1, y + 1);
            }
            else
            {
                result[0] = _boundary.GetBoundaryNeighbour(space, x, y, BoundaryDirection.Ne);
            }

            //Check SE neighbour
            if ( x + 1 < space.GetXLength() && y + 1 < space.GetYLength())
            {
                result[1] = space.GetCell(x + 1, y + 1);
            }
            else
            {
                result[1] = _boundary.GetBoundaryNeighbour(space, x, y, BoundaryDirection.Se);
            }

            //Check SW neighbour
            if (x + 1 < space.GetXLength() && y - 1 >= 0)
            {
                result[2] = space.GetCell(x + 1, y - 1);
            }
            else
            {
                result[2] = _boundary.GetBoundaryNeighbour(space, x, y, BoundaryDirection.Sw);
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

            return result;

         }
        public override string ToString()
        {
            return "Further Moore";
        }
    }

    
}
