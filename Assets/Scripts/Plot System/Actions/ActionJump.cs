using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionJump : AbstractAction
{
    public bool right = false;

    private bool jumped;

    public ActionJump(PlotPoint point, Level_Descriptor level, bool right): base(point, level)
    {
        isSpecific = true;
        this.right = right;
        jumped = false;
    }

    public override void Act(Unit_Controller unit)
    {
        if (!isDone)
        {
            int r_d = right? 1 : -1;
            //unit.SetMoveVector(new Vector3(unit.rigidbody.velocity.x * r_d, 0, 0));
            if (!jumped)
            {
                unit.Jump();
                jumped = true;
            }

            Vector3 distance = point.transform.position - unit.transform.position;

            unit.SetMoveVector(distance);

            if (distance.magnitude <= 0.3f)
                isDone = true;
        }
    }
}
