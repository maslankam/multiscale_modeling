using System;
using System.Drawing;
using System.Linq;
using Model;


namespace Model{
    
    public static class TransitionRule
    {
        public static Grain NextState(CelluralSpace space, int x, int y){
            if(space.currentState[x, y].GrainMembership == null){
                
                Cell[] neighbours = VonNeumanNeighborhood.Neighbours(
                    space.currentState, x, y, AbsorbingBoundary.BoundaryCondition);
                
                // Find the strongest neighbour. In case of tie 
                var top = neighbours.Where(c => c.GrainMembership != null).
                                    GroupBy(c => c.GrainMembership).
                                    OrderBy(g => g.Count).
                                    Select(g => g);

                throw new NotImplementedException();
            }
            else{
                return space.currentState[x, y].GrainMembership;
            }
        }
        
    }

    

}
