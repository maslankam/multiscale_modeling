using System;
using System.Drawing;

namespace Model{    
    public class Cell
    {
        public Microelement MicroelementMembership { get; set; }
        public CelluralSpace SpaceMembership{get; private set;}
        public int X { get; private set; }
        public int Y { get; private set; }


        public Cell(int x, int y)
        {
            this.X = x;
            this.Y = y;
            this.MicroelementMembership = null;
        }
        public Cell(int x, int y, Microelement microelement) : this(x,y)
        {
            this.MicroelementMembership = microelement;   
        }

        public void NextState( ITransitionRule transition, INeighbourhood neighbourhood, IBoundaryCondition _boundary){
            this.MicroelementMembership = 
        }

    }

    
}
