﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Model
{
    public class InclusionInitializer
    {
        public List<Inclusion> Initialize(int count, int minRadius, int maxRadius){
            var inclusions = new List<Inclusion>();
            var r = new Random();

            for(int i = 0; i < count; i++){
                var col = Color.FromArgb(0, r.Next(0,255), r.Next(0,255), r.Next(0,255));
                inclusions.Add( new Inclusion(i, 1, r.Next(minRadius, maxRadius), col));  //TODO: Use Utility.IColorGenerator
            }                                                                         // 1 is phase, for this momemnt no use of it
            
            return inclusions;
        }

    }
}
