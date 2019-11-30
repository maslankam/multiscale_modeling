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
                var x = r.Next(0, space.GetXLength() - 1);
                var y = r.Next(0, space.GetYLength() - 1);
                space.SetCellMembership(grain, x, y);
            }
        }

    }
}
