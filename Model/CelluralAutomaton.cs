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
        private readonly SimulationExecutor _executor;
        //private SpaceRenderingEngine _renderingEngine;
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
            if(minRadius > maxRadius) throw new ArgumentException("MinRadius cannot be greater than MaxRadius");
            if (transition == null || neighbourhood == null || boundary == null) throw new ArgumentNullException();
            _transition = transition;
            _neighbourhood = neighbourhood;
            _lastStepSpace = new CelluralSpace(size);
            _executor = new SimulationExecutor();
            _grainInitializer = new GrainInitializer();
            _inclusionInitializer = new InclusionInitializer();
            _grainSeeder = new GrainSeeder();
            _inclusionSeeder = new InclusionSeeder(boundary);

            Space = new CelluralSpace(size);
            PopulateSimulation(GrainsCount, inclusionsCount, minRadius, maxRadius);
        }

        //Constructor for opening saved state
        public CelluralAutomaton(
            CelluralSpace space,
            List<Grain> grains,
            List<Inclusion> inclusions,
            ITransitionRule transition,
            INeighbourhood neighbourhood,
            IBoundaryCondition boundary,
            int step)
        {
            if (transition == null     ||
                neighbourhood == null  ||
                boundary == null       ||
                space == null          ||
                grains == null         ||
                inclusions == null      ) throw new ArgumentNullException();

            _transition = transition;
            _neighbourhood = neighbourhood;
            
            _executor = new SimulationExecutor();
            _grainInitializer = new GrainInitializer();
            _inclusionInitializer = new InclusionInitializer();
            _grainSeeder = new GrainSeeder();
            _inclusionSeeder = new InclusionSeeder(boundary);

            _grainInitializer = new GrainInitializer();
            _inclusionInitializer = new InclusionInitializer();
            _grainSeeder = new GrainSeeder();
            _inclusionSeeder = new InclusionSeeder(boundary);

            Grains = grains;
            Inclusions = inclusions;

            Space = space;
            _lastStepSpace = space.Clone();

            _executor = new SimulationExecutor(step);
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
   }
}


