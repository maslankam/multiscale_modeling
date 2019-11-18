using System;
using System.Collections.Generic;

namespace Model{
    
    
    public interface ITransitionRule //TODO: This code probably probably won't be use, to be deleted ! THere is static class instead
    {
        Microelement NextState(Cell cell, Cell[] neighbours);
    }
    
}
