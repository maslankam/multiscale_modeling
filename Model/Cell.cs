using System;
using System.Drawing;

namespace Model{    
    public class Cell
    {
        public int? Phase 
        {
            get{return MicroelementMembership?.Phase ?? null;} 
            private set{}
        }
        public Microelement MicroelementMembership { get; set; }

        public Cell()
        {
            MicroelementMembership = null;
        }
        public Cell(Microelement microelement)
        {
            MicroelementMembership = microelement;   
        }

    }

    
}
