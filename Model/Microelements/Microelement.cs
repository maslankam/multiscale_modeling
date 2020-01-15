using System.Collections.Generic;
using System.Drawing;

namespace Model.Microelements{
    
    public abstract class Microelement
    {
        public abstract int? Phase{get; set;}
        public abstract Color Color {get; set;}
        public abstract int Id {get; set;}

        public abstract List<Cell> Members { get; set; }

        public virtual void Delete()
        {
          
        }

        public virtual void GetArea()
        {

        }

        public virtual void GetBorderLenght()
        {

        }



    }

}
