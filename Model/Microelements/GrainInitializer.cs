using System;
using System.Collections.Generic;
using System.Drawing;

namespace Model
{
    public class GrainInitializer
    {
        public List<Grain> Initialize(int count){
            var grains = new List<Grain>();
            var r = new Random();

            for(int i = 0; i < count; i++){
                //Check if cell is empty?? with while loop?
                grains.Add( new Grain(i, 0, Color.FromArgb(0, r.Next(0,255), r.Next(0,255), r.Next(0,255))) ); //TODO: Use Utility.IColorGenerator
            }                                                                                                  // Fixed 0 phase !
            
            return grains;
        }

    }
}
