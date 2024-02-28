using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractCamAction
{
    protected CinemachineVirtualCamera camera;

    public Level_Descriptor level;

    public int camWillActAfterThat; //When an action with this number is done the cam action starts

    public PlotRun dependedWith;

    protected bool isDone;

    public AbstractCamAction(Level_Descriptor level, int camWillActAfterThat, PlotRun dependedWith)
    {
        this.level = level;
        isDone = false;
        camera = level.camera;
        this.camWillActAfterThat = camWillActAfterThat;
        this.dependedWith = dependedWith;
    }

    public abstract void Act();

    public bool IsDone()
    {
        return isDone;
    }
}
