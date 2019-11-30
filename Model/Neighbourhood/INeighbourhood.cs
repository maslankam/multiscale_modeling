using System;
using System.Collections.Generic;
using Model;

namespace Model{
    
    
    public interface INeighbourhood //TODO: This code probably probably won't be use, to be deleted ! THere is static class instead
    {
       public Cell[] GetNeighbours(CelluralSpace space, int x, int y);
    }
    
}
