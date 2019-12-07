namespace Model{
    
    
    public interface ITransitionRule 
    {
        Microelement NextState(Cell cell, Cell[] neighbours);
    }
    
}
