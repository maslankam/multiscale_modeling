using Model.Neighbourhood;
using Model.Transition;

namespace Model.Executors
{
    public interface ISimulationExecutor
    {

        void NextState(CelluralSpace space, CelluralSpace lastSpace, ITransitionRule transition, INeighbourhood neighbourhood);
        int ReturnStep();
    }
}