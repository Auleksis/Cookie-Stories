using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSay : AbstractAction
{
    public string text;
    public bool unitWillStop;
    private bool textShown;
    private bool playerStop;

    public ActionSay(PlotPoint point, Level_Descriptor level, string text, bool unitWillStop, bool playerStop): base(point, level)
    {
        this.text = text;
        this.unitWillStop = unitWillStop;
        isSpecific = true;
        textShown = false;
        needStop = true;
        this.playerStop = playerStop;
    }

    public override void Act(Unit_Controller unit)
    {
        if (!textShown)
        {
            unit.uiManager.ShowMessage(text);
            textShown = true;

            if(playerStop)
                level.player.SetCanMove(false);
        }

        if (!unit.uiManager.IsSomethingShown() || !unitWillStop)
        {
            isDone = true;
            level.player.SetCanMove(true);
        }
    }
}
