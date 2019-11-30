using System;
using System.Collections.Generic;
using System.Drawing;

namespace Model{
    
    public class Grain : Microelement
    {
        public override int? Phase{get; set;}
        public override Color Color {get; set;}
        public override int Id{get; set;}

        public Grain(int id, int phase, Color color)
        {
            Id = id;
            Color = color;
            Phase = phase;
        }
    }

}
