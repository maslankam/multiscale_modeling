using System;
using System.Drawing;

namespace Model{
    public enum CellState
    {
        Empty,
        Active
    }
    
    public class Cell
    {
        public CellState State {get; set;}
        public Grain GrainMembership;
    }

    
}
