namespace Model.Boundary{
    
    public enum BoundaryDirection{
        N, Ne, E, Se , S, Sw, W, Nw
    }

    public interface IBoundaryCondition 
    {
        Cell GetBoundaryNeighbour(CelluralSpace space, int x, int y, BoundaryDirection direction);
    }

    
}
