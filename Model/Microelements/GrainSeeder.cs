using System;
using System.Collections.Generic;

namespace Model.Microelements
{
    public class GrainSeeder
    {
        public void Seed(CelluralSpace space, List<Grain> grains){
            var r = new Random();
            foreach(var grain in grains){
                
                int x,y;
                do
                { //TODO: infinite loop
                    x = r.Next(0, space.GetXLength() - 1);
                    y = r.Next(0, space.GetYLength() - 1);
                }while(space.GetCell(x,y).MicroelementMembership != null);
                
                space.SetCellMembership(grain, x, y);
            }
        }

    }
}
