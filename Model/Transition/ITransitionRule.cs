using Model.Microelements;

namespace Model.Transition{
    
    
    public interface ITransitionRule 
    {
        Microelement NextState(Cell cell, Cell[] neighbours);
    }
    
}
