using System.Drawing;

namespace Model.Microelements{
    
    public class Grain : Microelement
    {
        public sealed override int? Phase{get; set;}
        public sealed override Color Color {get; set;}
        public sealed override int Id{get; set;}

        public Grain(int id, int phase, Color color)
        {
            Id = id;
            Color = color;
            Phase = phase;
        }
    }

}
