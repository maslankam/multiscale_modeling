using System.Drawing;

namespace Model
{
    public class Inclusion : Microelement
    {
        public int? Radius{get; set;}
        public sealed override int? Phase {get; set;}

        public sealed override Color Color {get; set;}
        public sealed override int Id{get; set;}

        public Inclusion(int id, int phase, int radius, Color color)
        {
            Id = id;
            Color = color;
            Phase = phase;
            Radius = radius;
        }

    }
}
