using System.Drawing;
using System.Collections.Generic;
using Model.Neighbourhood;
using System.Linq;


namespace Model.Microelements{

    public class Grain : Microelement
    {
        public bool GenerateDetails { get; set; }
        public CellularAutomaton OwnerAutomaton { get; set; }
        public sealed override List<Cell> Members { get; set; }
        public sealed override int? Phase { get; set; }
        public sealed override Color Color { get; set; }
        public sealed override int Id { get; set; }

        public string Info
        {
            get => GenerateDetails ? $"Grain Id:{Id}, Phase:{Phase}, Area: {Area}, Border:{Border}" 
                                   : $"Grain Id:{Id}, Phase:{Phase}";
        }

        public int Area { get => Members.Count; }
        public int Border { get => (Members.Count((n) => n.isBorder == true)); }

        public Grain(int id, int phase, Color color)
        {
            Id = id;
            Color = color;
            Phase = phase;
            Members = new List<Cell>();
            GenerateDetails = false;
        }

        public override void Delete()
        {
            foreach (var member in Members)
            {
                member.MicroelementMembership = null;
                
            }
        }

        

    }

}
