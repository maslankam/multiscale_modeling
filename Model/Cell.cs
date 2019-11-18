using System;
using System.Drawing;

namespace Model{    
    public class Cell
    {
        public Microelement MicroelementMembership { get; set; }

        public Cell()
        {
            this.MicroelementMembership = null;
        }
        public Cell(Microelement microelement)
        {
            this.MicroelementMembership = microelement;   
        }

    }

    
}
