using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionWithAnotherUnit : AbstractAction
{
    protected Unit_Controller anotherUnit;

    public ActionWithAnotherUnit(PlotPoint point, Level_Descriptor level, Unit_Controller anotherUnit) : base(point, level)
    {
        this.anotherUnit = anotherUnit;
        isSpecific = true;
    }
}
