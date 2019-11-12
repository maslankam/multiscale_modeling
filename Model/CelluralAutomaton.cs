using System;
using System.Collections.Generic;
using System.Drawing;


namespace Model{
    public class CelluralAutomaton
    {
        public CelluralSpace Space { get; private set; }
        public List<Grain> Grains { get; private set; }
        public Bitmap View 
        {
            get { return Space.RenderCelluralSpace(); }
        }
        


   

        public CelluralAutomaton()
        {
            this.Space = new CelluralSpace(500); //TODO: replace magic number with resizable control
            this.Grains = new List<Grain>
            {  //TODO: Temporary solution to fix 3 grains !!!!
                new Grain(0, Color.Green), 
                new Grain(1, Color.Yellow) , 
                new Grain(2, Color.Blue)
            };

            this.Space.currentState[100, 100].GrainMembership = Grains[0];
            this.Space.currentState[200, 200].GrainMembership = Grains[1];
            this.Space.currentState[300, 300].GrainMembership = Grains[2];


        }

        
        

    }

    
}
