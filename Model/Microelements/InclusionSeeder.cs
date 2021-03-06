using System;
using System.Collections.Generic;
using System.Linq;
using Model.Boundary;

namespace Model.Microelements
{
    public class InclusionSeeder
    {
        private readonly InclusionExecutor _executor;

        public InclusionSeeder(IBoundaryCondition boundary){
            _executor = new InclusionExecutor(boundary);
        }


        public void Seed(CelluralSpace space, List<Inclusion> inclusions){
            var r = new Random();
            foreach(var inclusion in inclusions){
                var xCenter = r.Next(0, space.GetXLength() - 1);
                var yCenter = r.Next(0, space.GetYLength() - 1);
                space.SetCellMembership(inclusion, xCenter, yCenter);
            }

            int? maxRadius = (from i in inclusions
                             select i.Radius).Max();

            if(maxRadius.HasValue)
            {
                _executor.Grow(space, maxRadius.Value);
            }
        }
    }
}
