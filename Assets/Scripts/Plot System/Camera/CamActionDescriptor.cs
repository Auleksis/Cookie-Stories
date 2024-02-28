using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CamActionType { NONE, TRANSLATE, BACK_TO_PLAYER, WAIT };
[Serializable]
public class CamActionDescriptor
{
    public CamActionType type = CamActionType.NONE;

    public GameObject lookTo;

    public float waitForSeconds;

    public PlotRun dependedWith = null;

    public int camActionWillStartAfterPoint = 0;
}
