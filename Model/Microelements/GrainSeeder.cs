using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Model
{
    public class GrainSeeder
    {
        public void Seed(CelluralSpace space, List<Grain> grains){
            var r = new Random();
            foreach(var grain in grains){
                space.SetCellMembership(grain, r.Next(0, space.GetXLength() - 1), r.Next(0, space.GetYLength() - 1));
            }
        }

    }
}
