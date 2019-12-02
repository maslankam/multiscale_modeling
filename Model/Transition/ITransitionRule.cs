using System;
using System.Collections.Generic;

namespace Model{
    
    
    public interface ITransitionRule 
    {
        Microelement NextState(Cell cell, Cell[] neighbours);
    }
    
}
