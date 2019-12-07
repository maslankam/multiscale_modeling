using System;
using System.Collections.Generic;
using Model.Boundary;
using Model.Executors;
using Model.Microelements;
using Model.Neighbourhood;
using Model.Transition;


namespace Model{
    public class CellularAutomaton {

        public List<Grain> Grains { get; private set; }
        public List<Inclusion> Inclusions { get; private set; }

        public CelluralSpace Space { get; }
        public int Step => this._executor.ReturnStep();

        private CelluralSpace _lastStepSpace;
        private readonly ITransitionRule _transition;
        private readonly INeighbourhood _neighbourhood;
        private readonly ISimulationExecutor _executor;
        //private SpaceRenderingEngine _renderingEngine;
        private readonly GrainInitializer _grainInitializer;
        private readonly InclusionInitializer _inclusionInitializer;
        private readonly GrainSeeder _grainSeeder;
        private readonly InclusionSeeder _inclusionSeeder;

        public CellularAutomaton(int size,
            int grainsCount,
            int inclusionsCount,
            int minRadius,
            int maxRadius,
            ITransitionRule transition,
            INeighbourhood neighbourhood,
            IBoundaryCondition boundary,
            ISimulationExecutor executor)
        {
            if(minRadius > maxRadius) throw new ArgumentException("MinRadius cannot be greater than MaxRadius");
            if (transition == null || neighbourhood == null || boundary == null) throw new ArgumentNullException();
            _transition = transition;
            _neighbourhood = neighbourhood;
            _lastStepSpace = new CelluralSpace(size);
            _executor = executor;
            _grainInitializer = new GrainInitializer();
            _inclusionInitializer = new InclusionInitializer();
            _grainSeeder = new GrainSeeder();
            _inclusionSeeder = new InclusionSeeder(boundary);

            Space = new CelluralSpace(size);
            PopulateSimulation(grainsCount, inclusionsCount, minRadius, maxRadius);
        }

        //Constructor for opening saved state
        public CellularAutomaton(
            CelluralSpace space,
            List<Grain> grains,
            List<Inclusion> inclusions,
            ITransitionRule transition,
            INeighbourhood neighbourhood,
            IBoundaryCondition boundary, ISimulationExecutor executor)
        {
            if (transition == null     ||
                neighbourhood == null  ||
                boundary == null       ||
                space == null          ||
                grains == null         ||
                inclusions == null      ) throw new ArgumentNullException();

            _transition = transition;
            _neighbourhood = neighbourhood;
            
            
            _grainInitializer = null;
            _inclusionInitializer = null;
            _grainSeeder = null;
            _inclusionSeeder = null;

            Grains = grains;
            Inclusions = inclusions;

            Space = space;
            _lastStepSpace = space.Clone();

            _executor = executor;
        }
        
        public void NextStep()
        {
            _lastStepSpace = Space.Clone();
            _executor.NextState(Space, _lastStepSpace, _transition, _neighbourhood);
        }

        public void PopulateSimulation(int grainsCount, int inclusionsCount, int minRadius, int maxRadius)
        {
            Grains = _grainInitializer.Initialize(grainsCount);
            _grainSeeder.Seed(Space, Grains);
            Inclusions = _inclusionInitializer.Initialize(inclusionsCount, minRadius, maxRadius);
            _inclusionSeeder.Seed(Space, Inclusions);
        }
   }
}


