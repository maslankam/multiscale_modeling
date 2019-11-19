using System;
using System.Collections.Generic;
using System.Drawing;


namespace Model{
    public class CelluralAutomaton {

        public List<Grain> Grains { get; private set; }
        public List<Inclusion> Inclusions { get; private set; }

        public CelluralSpace Space { get; private set; }
        public int Step
        {
            get { return this._executor.Step; }
            private set { }
        }

        private CelluralSpace _lastStepSpace;
        private readonly ITransitionRule _transition;
        private readonly INeighbourhood _neighbourhood;
        private readonly IBoundaryCondition _boundary;
        private readonly SimulationExecutor _executor;
        private SpaceRenderingEngine _renderingEngine;
        private readonly GrainInitializer _grainInitializer;
        private readonly InclusionInitializer _inclusionInitializer;
        private readonly GrainSeeder _grainSeeder;
        private readonly InclusionSeeder _inclusionSeeder;

        public CelluralAutomaton(int size,
            int GrainsCount,
            int inclusionsCount,
            int minRadius,
            int maxRadius,
            ITransitionRule transition,
            INeighbourhood neighbourhood,
            IBoundaryCondition boundary)
        {
            if (transition == null || neighbourhood == null || boundary == null) throw new ArgumentNullException();
            _transition = transition;
            _neighbourhood = neighbourhood;
            _boundary = boundary;
            Space = new CelluralSpace(size);
            _lastStepSpace = new CelluralSpace(size);
            _executor = new SimulationExecutor();
            _grainInitializer = new GrainInitializer();
            _inclusionInitializer = new InclusionInitializer();
            _grainSeeder = new GrainSeeder();
            _inclusionSeeder = new InclusionSeeder(boundary);

            PopulateSimulation(GrainsCount, inclusionsCount, minRadius, maxRadius);
        }

        public void NextStep()
        {
            _lastStepSpace = Space.Clone();
            _executor.NextState(Space, _lastStepSpace, _transition, _neighbourhood);
        }

        public void PopulateSimulation(int GrainsCount, int inclusionsCount, int minRadius, int maxRadius)
        {
            Grains = _grainInitializer.Initialize(GrainsCount);
            _grainSeeder.Seed(Space, Grains);
            Inclusions = _inclusionInitializer.Initialize(inclusionsCount, minRadius, maxRadius);
            _inclusionSeeder.Seed(Space, Inclusions);
        }

        // getSpaceImage(){
            //SpaceRenderingEngine.Render(Space);
           //}

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

