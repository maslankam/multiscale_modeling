using System;
using System.Collections.Generic;
using System.Drawing;


namespace Model{
   public class CelluralAutomaton{

       public List<Grain> Grains{get; private set;}
       public List<Inclusion> Inclusions{get; private set;}

       public CelluralSpace Space {get; private set;} 
       public int Step
       {
           get{ return this._executor.Step;}
           private set{} 
        }

       private CelluralSpace _lastStepSpace;
       private ITransitionRule _transition;
       private INeighbourhood _neighbourhood;
       private IBoundaryCondition _boundary;
       private SimulationExecutor _executor;
       private SpaceRenderingEngine _renderingEngine;
       private GrainInitializer _grainInitializer;
       private InclusionInitializer _inclusionInitializer;
       private GrainSeeder _grainSeeder;
       private InclusionSeeder _inclusionSeeder;

       public CelluralAutomaton(int size, ITransitionRule transition, INeighbourhood neighbourhood, IBoundaryCondition boundary)
       {
           if(transition == null || neighbourhood == null || boundary == null) throw new ArgumentNullException();
           this._transition = transition;
           this._neighbourhood = neighbourhood;
           this._boundary = boundary;
           this.Space = new CelluralSpace(size);
           this._lastStepSpace = new CelluralSpace(size);
           this._executor = new SimulationExecutor();
           this._grainInitializer = new GrainInitializer();
           this._inclusionInitializer = new InclusionInitializer();
           this._grainSeeder = new GrainSeeder();
           this._inclusionSeeder = new InclusionSeeder(boundary);
       }

        public void NextStep()
        {
            _lastStepSpace = Space.Clone();
            _executor.NextState(Space, _transition, _neighbourhood);
        }

        public void PopulateSimulation()
        {
            //_grainGenerator.Generate(int number)
            //_inclusionGenerator(int number, int min, int max);
        }

        public void Reset()
        {

        }


 

   }
   
   
   
   
   
   
   
   
   
   /* public class CelluralAutomaton
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

        
        

    }*/

    
}

