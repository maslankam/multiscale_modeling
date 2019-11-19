using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Model
{
    public class Inclusion : Microelement
    {
        public static int MinRadius{get; private set;} //TODO: after reseting automaton this static field must be reset too
        public static int MaxRadius{get; private set;}
        public int? Radius{get; set;}
        public override int? Phase {get; set;}

        public override Color Color {get; set;}
        public override int Id{get; set;}

        public Inclusion(int id, int phase, int radius, Color color)
        {
            Id = id;
            Color = color;
            Phase = phase;
            Radius = radius;
            if(radius > MaxRadius) MaxRadius = radius;
            if(radius < MinRadius || MinRadius == 0) MinRadius = radius;
        }

    }
}
