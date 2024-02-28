using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionType {NONE, JUMP, SAY, TELL, STOP_MOVEMENT, START_MOVEMENT, ESCORT, SPAWN, MOVE, WAIT}
[Serializable]
public class ActionDescriptor
{
    public ActionType type = ActionType.NONE;

    public GameObject plotPoint;

    public Unit_Controller unit;

    public string text;

    public string[] playerAnswers;

    public bool right;

    public bool stopUntillAllTextIsShown;

    public bool playerStop;

    public float maxDistance = 3;

    public float waitForSeconds;

    public ActionDescriptor(ActionType type, GameObject plotPoint)
    {
        this.type = type;
        this.plotPoint = plotPoint;
    }
}
