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
            this.Space = new CelluralSpace(3); //TODO: replace magic number with resizable control
            


        }

        
        

    }

    
}
