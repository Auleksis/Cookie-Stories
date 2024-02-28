using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionEscort : AbstractAction
{
    public Unit_Controller escorted;
    public float maxDistance;

    public ActionEscort(PlotPoint point, Level_Descriptor level, Unit_Controller escorted, float maxDistance): base(point, level)
    {
        this.escorted = escorted;
        this.maxDistance = maxDistance;
        isSpecific = true;
    }

    public override void Act(Unit_Controller unit)
    {
        if (!isDone)
        {
            Vector3 dist = unit.transform.position - escorted.transform.position;

            Vector3 distance = point.transform.position - unit.transform.position;

            if (dist.magnitude > maxDistance)
                unit.SetMoveVector(new Vector3(0, 0, 0));
            else
                unit.SetMoveVector(distance);

            if (distance.magnitude <= 0.3f)
                isDone = true;
        }
    }
}
