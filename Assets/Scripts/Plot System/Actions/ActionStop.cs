using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionStop : ActionWithAnotherUnit
{
    public ActionStop(PlotPoint point, Level_Descriptor level, Unit_Controller anotherUnit) : base(point, level, anotherUnit)
    {
    }

    public override void Act(Unit_Controller unit)
    {
        anotherUnit.SetCanMove(false);
        isDone = true;
    }
}
