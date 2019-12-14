namespace Model.Neighbourhood{
    
    
    public interface INeighbourhood //TODO: This code probably probably won't be use, to be deleted ! THere is static class instead
    {
       Cell[] GetNeighbours(CelluralSpace space, int x, int y);
    }
    
}
