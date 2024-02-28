using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBackToPlayer : AbstractCamAction
{
    public ActionBackToPlayer(Level_Descriptor level, int camWillActAfterThat, PlotRun dependedWith) : base(level, camWillActAfterThat, dependedWith)
    {
    }

    public override void Act()
    {
        if (dependedWith.GetCurrentStep() > camWillActAfterThat)
        {
            camera.Follow = level.player.transform;
            isDone = true;
        }
    }
}
