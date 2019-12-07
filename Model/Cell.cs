using Model.Microelements;

namespace Model{    
    public class Cell
    {
        public int? Phase 
        {
            get{return MicroelementMembership?.Phase;}
        }
        public Microelement MicroelementMembership { get; set; }

        public Cell()
        {
            MicroelementMembership = null;
        }
        public Cell(Microelement microelement)
        {
            MicroelementMembership = microelement;
        }

    }

    
}
