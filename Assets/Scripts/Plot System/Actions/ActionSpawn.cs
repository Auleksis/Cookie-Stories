using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSpawn : ActionWithAnotherUnit
{
    public ActionSpawn(PlotPoint point, Level_Descriptor level, Unit_Controller anotherUnit) : base(point, level, anotherUnit)
    {

    }

    public override void Act(Unit_Controller unit)
    {
        anotherUnit.transform.position = point.transform.position;
        isDone = true;
    }
}
