using System;
using System.Collections.Generic;
using System.Drawing;


namespace Model{
    public class CelluralAutomaton
    {
        public CelluralSpace Space { get; private set; }
        public List<Grain> Grains { get; private set; }
        
        
        public CelluralAutomaton()
        {
            this.Space = new CelluralSpace(500); //TODO: replace magic number with resizable control
        }

        public CelluralAutomaton(int spaceSize)
        {
            this.Space = new CelluralSpace(spaceSize);
        }

        
        

    }

    
}

