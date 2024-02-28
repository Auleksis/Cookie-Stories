using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMove : AbstractAction
{
    public ActionMove(PlotPoint point, Level_Descriptor level) : base(point, level)
    {
        isSpecific = true;
    }

    public override void Act(Unit_Controller unit)
    {
        if (!isDone)
        {
            Vector3 distance = point.transform.position - unit.transform.position;

            unit.SetMoveVector(distance);

            if (distance.magnitude <= 0.3f)
                isDone = true;
        }
    }
}
