using System;
using System.Drawing;

namespace Model{    
    public class Cell
    {
        public Grain GrainMembership { get; set; }
        public int X { get; private set; }
        public int Y { get; private set; }

        public Cell(int x, int y)
        {
            this.X = x;
            this.Y = y;
            this.GrainMembership = null;
        }
        public Cell(int x, int y, Grain grain) : this(x,y)
        {
            this.GrainMembership = grain;   
        }

    }

    
}
