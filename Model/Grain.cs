using System;
using System.Collections.Generic;
using System.Drawing;

namespace Model{
    
    public class Grain
    {
        public static List<Grain> List { get; private set; }
        public Color Color {get; set;}
        public int Id;

        public Grain()
        {
            this.Color = Color.White;
            this.Id = null;
        }

        public Grain(int id, Color color)
        {
            this.Id = id;
            this.Color = color;
        }
    }

}
