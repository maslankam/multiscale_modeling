using System;
using System.Drawing;

namespace Model{    
    public class Cell
    {
        public int Phase 
        {
            get{return MicroelementMembership.Phase;} 
            private set{}
        }
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
