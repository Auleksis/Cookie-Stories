using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionAllowGo : ActionWithAnotherUnit
{
    public ActionAllowGo(PlotPoint point, Level_Descriptor level, Unit_Controller anotherUnit) : base(point, level, anotherUnit)
    {
    }

    public override void Act(Unit_Controller unit)
    {
        anotherUnit.SetCanMove(true);
        isDone = true;
    }
}
