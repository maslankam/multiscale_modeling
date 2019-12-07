namespace Model
{
    public interface ISimulationExecutor
    {

        void NextState(CelluralSpace space, CelluralSpace lastSpace, ITransitionRule transition, INeighbourhood neighbourhood);
        int ReturnStep();
    }
}