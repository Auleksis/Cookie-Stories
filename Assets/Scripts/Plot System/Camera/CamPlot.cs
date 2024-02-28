using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamPlot : MonoBehaviour
{
    public CamActionDescriptor [] actionDescriptors;

    public Level_Descriptor level;

    public CamPlotRun plotRun;

    Queue<AbstractCamAction> camActions;

    private void Start()
    {
        camActions = new Queue<AbstractCamAction>();
        AbstractCamAction camAction = null;

        for(int i = 0; i < actionDescriptors.Length; i++)
        {
            CamActionDescriptor actionDescriptor = actionDescriptors[i];

            switch (actionDescriptor.type)
            {
                case CamActionType.TRANSLATE:
                    camAction = new ActionTranslate(level, actionDescriptor.camActionWillStartAfterPoint, actionDescriptor.dependedWith, actionDescriptor.lookTo);
                    break;
                case CamActionType.WAIT:
                    camAction = new ActionCamWait(level, actionDescriptor.camActionWillStartAfterPoint, actionDescriptor.dependedWith, actionDescriptor.waitForSeconds);
                    break;
                case CamActionType.BACK_TO_PLAYER:
                    camAction = new ActionBackToPlayer(level, actionDescriptor.camActionWillStartAfterPoint, actionDescriptor.dependedWith);
                    break;
            }

            camActions.Enqueue(camAction);
        }

        plotRun.Init(camActions);
    }
}
