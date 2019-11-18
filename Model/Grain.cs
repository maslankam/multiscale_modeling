using System;
using System.Collections.Generic;
using System.Drawing;

namespace Model{
    
    public class Grain : Microelement
    {
        public override Color Color {get; set;}
        public override int Id{get; set;}

        public Grain(int id, Color color)
        {
            this.Id = id;
            this.Color = color;
        }
    }

}
