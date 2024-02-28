using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionCamWait : AbstractCamAction
{
    private float waitForSeconds;
    private bool isStarted;
    public ActionCamWait(Level_Descriptor level, int camWillActAfterThat, PlotRun dependedWith, float waitForSeconds) : base(level, camWillActAfterThat, dependedWith)
    {
        this.waitForSeconds = waitForSeconds;
        isStarted = false;
    }

    public override void Act()
    {
        if (!isStarted)
        {
            level.clock.StartCoroutine(Wait());
            isStarted = true;
        }            
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(waitForSeconds);
        isDone = true;
    }
}
