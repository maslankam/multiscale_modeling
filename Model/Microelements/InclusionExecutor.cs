using Model.Boundary;
using Model.Neighbourhood;
using Model.Transition;

namespace Model.Microelements
{
    class InclusionExecutor
    {
        private ITransitionRule _transition;
        private readonly INeighbourhood _neighbourhood;

        public InclusionExecutor(IBoundaryCondition boundary)
        {
            _neighbourhood = new  PentagonNeighbourhood(boundary);
        }

        public void Grow(CelluralSpace space, int maxRadius)
        {
            //In fact inclusions don't grow but to obtain round shape pentagonal neighbourhood growing fits very weel,
            //This approach also resolves the problem with periodic boundary
            //But if inclusions appears next to each other and collide then they won't be perfect spherical. May apply radius checking for initializing??

            for (int step = 1; step < maxRadius; step++)
            {
                var previousSpace = space.Clone();

                for (int i = 0; i < space.GetXLength(); i++)
                {
                    for (int j = 0; j < space.GetYLength(); j++)
                    {
                        Cell[] neighbours = _neighbourhood.GetNeighbours(previousSpace, i, j);
                        _transition = new InclusionGrowthRule(step);
                        var element = _transition.NextState(space.GetCell(i, j), neighbours);
                        space.SetCellMembership(element, i, j);
                    }
                }
            }


        }
    }
}
