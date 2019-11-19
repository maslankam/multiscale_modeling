using System;
using System.Collections.Generic;
using System.Drawing;

namespace Model{
    
    public abstract class Microelement
    {
        public abstract int? Phase{get; set;}
        public abstract Color Color {get; set;}
        public abstract int Id {get; set;}

        
    }

}
