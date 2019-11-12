using System;
using System.Drawing;

namespace Model{
    
    public class Grain
    {
        public Color Color {get; set;}
        public readonly int Id;
    }

    public class EmptyGrain : Grain
    {
        public EmptyGrain(){
            this.Color = Color.White;
            this.Id = -1;
        }
    }
}
