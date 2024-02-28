using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlotPoint : MonoBehaviour
{
    [HideInInspector]public int id;

    private AbstractAction action;

    public void Act(Unit_Controller unit)
    {
        action.Act(unit);
    }

    public void SetAction(AbstractAction action)
    {
        this.action = action;
    }

    public bool IsSpecific()
    {
        return action != null || (action != null && !action.isSpecific);
    }

    public ActionType GetActionType()
    {
        return action.type;
    }

    public bool IsActionDone()
    {
        return action.isDone;
    }

    public bool NeedStop()
    {
        return !action.isDone && action.needStop;
    }
}
