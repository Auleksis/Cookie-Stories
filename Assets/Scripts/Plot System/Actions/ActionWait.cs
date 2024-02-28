using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionWait : AbstractAction
{
    public float waitForSeconds;
    public bool isStarted;
    public Unit_Controller unit;
    public ActionWait(PlotPoint point, Level_Descriptor level, float waitForSeconds) : base(point, level)
    {
        this.waitForSeconds = waitForSeconds;
        isSpecific = true;
        isStarted = false;
    }

    public override void Act(Unit_Controller unit)
    {
        if (!isStarted)
        {
            this.unit = unit;
            level.clock.StartCoroutine(Wait(unit));
            isStarted = true;
        }
    }

    private IEnumerator Wait(Unit_Controller unit)
    {
        unit.SetCanMove(false);
        yield return new WaitForSeconds(waitForSeconds);
        unit.SetCanMove(true);
        isDone = true;
    }
}
