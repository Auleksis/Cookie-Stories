using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractAction
{
    public ActionType type;
    public bool isDone = false;
    public bool isSpecific = false;
    public PlotPoint point; //NEXT POINT. YOU CAN USE TO MAKE YOUR ACTION BETTER
    public bool needStop = false;
    public Level_Descriptor level;

    public AbstractAction(PlotPoint point, Level_Descriptor level)
    {
        this.point = point;
        this.level = level;
    }

    public abstract void Act(Unit_Controller unit);
}
