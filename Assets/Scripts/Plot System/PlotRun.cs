using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum START_MODE { ON_AWAKE, ON_PLOT_FINISHED, ON_ACTION_DONE}
public class PlotRun : MonoBehaviour
{
    public Unit_Controller unit;
    public int startPointNumber = 0;
    public START_MODE startMode = START_MODE.ON_AWAKE;

    public PlotRun previousPlotStartingThisOne = null;

    public AbstractAction previousActionStartingThisOne = null;

    [HideInInspector]public Plot plot;


    private PlotPoint currentPoint;
    private PlotPoint nextPoint;

    private int currentStep = 0;

    private bool isRunning = false;

    private bool isFinished = false;

    private void Start()
    {
        if (startMode == START_MODE.ON_AWAKE)
            isRunning = true;
    }

    public void Init()
    {
        currentStep = startPointNumber;

        for(int i = 0; i < startPointNumber + 1; i++)
        {
            currentPoint = plot.NextPoint();
            nextPoint = plot.NextPoint();
        }
    }

    private void Update()
    {
        if (!isRunning && startMode == START_MODE.ON_PLOT_FINISHED)
            CheckPreviousPlotFinished();

        if (!isRunning)
            return;

        if (currentPoint != null)
        {
            currentPoint.Act(unit);

            if (currentPoint.IsActionDone())
                GetNewPoint();
        }

        if(nextPoint == null || (currentPoint.IsSpecific() && currentPoint.NeedStop()))
            unit.SetMoveVector(new Vector3(0, 0, 0));        
    }

    private bool NeedNext()
    {
        Vector3 distance = nextPoint.transform.position - unit.transform.position;
        //Debug.Log(distance.magnitude);
        return distance.magnitude <= 0.3f;
    }

    private void JustMove()
    {
        Vector3 distance = nextPoint.transform.position - unit.transform.position;
        unit.SetMoveVector(distance);
    }

    private void GetNewPoint()
    {
        if (nextPoint != null && currentPoint != null)
        {
            Vector3 distance = nextPoint.transform.position - unit.transform.position;
            Debug.Log(distance.magnitude);
            Debug.Log("Step " + currentStep + " is done");
        }

        currentPoint = nextPoint;
        nextPoint = plot.NextPoint();
        currentStep++;

        if(currentPoint == null)
        {
            isRunning = false;
            isFinished = true;
        }
    }

    public bool IsFinished()
    {
        return isFinished;
    }

    private void CheckPreviousPlotFinished()
    {
        if (previousPlotStartingThisOne != null && previousPlotStartingThisOne.IsFinished())
        {
            isRunning = true;
        }

        else if(previousActionStartingThisOne != null && previousActionStartingThisOne.isDone)
        {
            isRunning = true;
        }
    }

    public int GetCurrentStep()
    {
        return currentStep;
    }
}
