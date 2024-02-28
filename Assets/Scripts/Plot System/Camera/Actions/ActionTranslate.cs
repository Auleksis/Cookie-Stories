using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionTranslate : AbstractCamAction
{
    GameObject lookTo;

    public ActionTranslate(Level_Descriptor level, int camWillActAfterThat, PlotRun dependedWith, GameObject lookTo) : base(level, camWillActAfterThat, dependedWith)
    {
        this.lookTo = lookTo;
    }

    public override void Act()
    {
        camera.Follow = lookTo.transform;
        isDone = true;
    }
}
