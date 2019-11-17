using System;
using System.Drawing;

namespace Model{    
    public class Cell
    {
        public Grain GrainMembership { get; set; }
        public int X { get; private set; }
        public int Y { get; private set; }

        public Cell()
        {
            this.GrainMembership = null;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }

    
}
