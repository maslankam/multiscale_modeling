using System.Collections.Generic;
using System.Drawing;

namespace Model.Microelements
{
    public class Inclusion : Microelement
    {
        public int? Radius{get; }
        public sealed override int? Phase {get; set;}

        public sealed override Color Color {get; set;}
        public sealed override int Id{get; set;}
        public sealed override List<Cell> Members { get; set; }

        public Inclusion(int id, int phase, int radius, Color color)
        {
            Id = id;
            Color = color;
            Phase = phase;
            Radius = radius;
            Members = new List<Cell>();
        }

    }
}
