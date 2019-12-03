using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace Model
{
    public interface ISimulationExecutor
    {

        void NextState(CelluralSpace space, CelluralSpace lastSpace, ITransitionRule transition, INeighbourhood neighbourhood);
        int ReturnStep();
    }
}