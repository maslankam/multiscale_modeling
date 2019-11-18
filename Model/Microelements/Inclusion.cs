using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Model
{
    public class Inclusion : Microelement
    {
        public int Phase {get; set;}

        public override Color Color {get; set;}
        public override int Id{get; set;}

        public Inclusion(int id, Color color)
        {
            this.Id = id;
            this.Color = color;
        }
    }
}
