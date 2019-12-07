using System.Drawing;

namespace Model.Microelements{
    
    public abstract class Microelement
    {
        public abstract int? Phase{get; set;}
        public abstract Color Color {get; set;}
        public abstract int Id {get; set;}

        
    }

}
