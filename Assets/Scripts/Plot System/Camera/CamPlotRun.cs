using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamPlotRun : MonoBehaviour
{
    private Queue<AbstractCamAction> actionQueue;

    public START_MODE startMode = START_MODE.ON_PLOT_FINISHED;

    public PlotRun previousPlotStartingThisOne = null;

    public AbstractAction previousActionStartingThisOne = null;

    private bool isRunning;

    private AbstractCamAction currentAction;

    public void Init(Queue<AbstractCamAction> actionQueue)
    {
        this.actionQueue = actionQueue;
        currentAction = actionQueue.Dequeue();
    }

    private void Start()
    {
        if (startMode == START_MODE.ON_AWAKE)
            isRunning = true;
    }

    private void Update()
    {
        if (!isRunning && startMode == START_MODE.ON_PLOT_FINISHED)
            CheckPreviousPlotFinished();

        if (!isRunning)
            return;

        if (currentAction != null)
        {
            currentAction.Act();

            if (currentAction.IsDone())
                GetNextAction();
        }
    }

    private void GetNextAction()
    {
        if(actionQueue.Count > 0)
            currentAction = actionQueue.Dequeue();
        else
            currentAction = null;
    }


    private void CheckPreviousPlotFinished()
    {
        if (previousPlotStartingThisOne != null && previousPlotStartingThisOne.IsFinished())
        {
            isRunning = true;
        }

        else if (previousActionStartingThisOne != null && previousActionStartingThisOne.isDone)
        {
            isRunning = true;
        }
    }
}
