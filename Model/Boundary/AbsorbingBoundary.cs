namespace Model.Boundary{
    
    
    public class AbsorbingBoundary : IBoundaryCondition
    {
        public string Name => this.ToString();

        // AbsorbingBoundary always return null Cell 
        /// |nl|nl|nl|nl|nl|
        /// |nl|00|01|02|nl|
        /// |nl|10|11|12|nl|
        /// |nl|20|21|22|nl|
        /// |nl|nl|nl|nl|nl|
        public Cell GetBoundaryNeighbour(CelluralSpace space, int x, int y, BoundaryDirection direction) { 
            return null;
        }

        public override string ToString()
        {
            return "AbsorbingBoundary";
        }
    }

    
}
