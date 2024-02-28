using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Plot : MonoBehaviour
{
    //редачим через инспектор массив
    //Затем автоматически создаётся очередь на основе массива

    //НУЖНО ДОРАБОТАТЬ ФИЗИКУ!
    //ДОБАВИТЬ ЗАВИСИМОСТЬ ОТ МАССЫ ПЕРСОНАЖА И СИЛЫ ЕГО ПРЫЖКА
    public GameObject plotPointPrefab;

    public ActionDescriptor[] points;

    public Level_Descriptor level;

    public PlotRun plotRun;

    private List<ActionDescriptor> plotPoints;

    private static float debug_radius = 0.2f;

    private Queue<PlotPoint> pointsQueue;

    private int pointID = 0;

    void Start()
    {
        pointsQueue = new Queue<PlotPoint>();
        AbstractAction abstractAction = null;

        for(int i = 0; i < points.Length; i++)
        {
            ActionDescriptor action = points[i];
            PlotPoint p = action.plotPoint.GetComponent<PlotPoint>();

            ActionDescriptor action1 = null;
            PlotPoint p1 = null;
            if (i + 1 < points.Length)
            {
                action1 = points[i + 1];
                p1 = action1.plotPoint.GetComponent<PlotPoint>();
            }

            switch (action.type)
            {
                case ActionType.JUMP:
                    abstractAction = new ActionJump(p1, level, action.right);
                    break;
                case ActionType.SAY:
                    abstractAction = new ActionSay(p, level, action.text, action.stopUntillAllTextIsShown, action.playerStop);
                    break;
                case ActionType.ESCORT:
                    abstractAction = new ActionEscort(p1, level, level.player, 3);
                    break;
                case ActionType.START_MOVEMENT:
                    abstractAction = new ActionAllowGo(p, level, action.unit);
                    break;
                case ActionType.STOP_MOVEMENT:
                    abstractAction = new ActionStop(p, level, action.unit);
                    break;
                case ActionType.SPAWN:
                    abstractAction = new ActionSpawn(p, level, action.unit);
                    break;
                case ActionType.MOVE:
                    abstractAction = new ActionMove(p1, level);
                    break;
                case ActionType.WAIT:
                    abstractAction = new ActionWait(p, level, action.waitForSeconds);
                    break;
            }

            p.SetAction(abstractAction);
            pointsQueue.Enqueue(p);
        }

        plotRun.plot = this;
        plotRun.Init();
    }

    public PlotPoint NextPoint()
    {
        if (pointsQueue.Count > 0)
            return pointsQueue.Dequeue();
        return null;
    }

    public void AddPoint()
    {
        
        GameObject newPoint = Instantiate(plotPointPrefab, transform);

        if (points.Length == 0)
            newPoint.transform.position = new Vector3(0, 0, 0);
        else
            newPoint.transform.position = points[points.Length - 1].plotPoint.transform.position + new Vector3(1, 0, 0);

        ActionDescriptor descriptor = new ActionDescriptor(ActionType.NONE, newPoint);

        plotPoints.Add(descriptor);

        points = plotPoints.ToArray();
    }

    public void DeletePoint()
    {
        if (plotPoints.Count > 0)
        {
            GameObject p = plotPoints[plotPoints.Count - 1].plotPoint;
            plotPoints.RemoveAt(plotPoints.Count - 1);
            DestroyImmediate(p.gameObject);
            points = plotPoints.ToArray();
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        pointID = 0;
        if (plotPoints == null)
        {
            plotPoints = new List<ActionDescriptor>(points);
        }

        foreach (ActionDescriptor a in points)
        {
            PlotPoint point = a.plotPoint.GetComponent<PlotPoint>();
            Handles.color = Color.green;
            point.id = pointID;
            pointID++;

            Handles.DrawSolidDisc(point.transform.position, new Vector3(0, 0, 1), debug_radius);
            Handles.Label(point.transform.position - new Vector3(0, debug_radius, 0), point.id.ToString());
        }
        if (plotPoints.Count > 1)
        {
            for (int i = 1; i < points.Length; i++)
            {
                PlotPoint point1 = points[i - 1].plotPoint.GetComponent<PlotPoint>();
                PlotPoint point2 = points[i].plotPoint.GetComponent<PlotPoint>();

                if (points[i - 1].type != ActionType.JUMP)
                    Handles.DrawLine(point1.transform.position, point2.transform.position);
                else
                {
                    List<Vector3> dots = new List<Vector3>();

                    float time_step = 0.01f;

                    float V_y = (Level_Descriptor.unit_jump_force / Level_Descriptor.unit_mass) * time_step * 0.93f;

                    float simulationTime = 4f; //seconds

                    RaycastHit2D collidable = Physics2D.Raycast(point1.transform.position, new Vector2(0, -1), Mathf.Infinity, 1 << LayerMask.NameToLayer("Collidable"));

                    float jump_level = collidable.distance;

                    Vector3 current = point1.transform.position;

                    float t = 0;

                    dots.Add(current);

                    int right = points[i - 1].right ? 1 : -1;

                    do
                    {
                        t += time_step;
                        current = new Vector3(point1.transform.position.x + right * Level_Descriptor.unit_max_velocity * t * 0.41f, point1.transform.position.y + V_y * t - Level_Descriptor.gravity_scale * t * t, 0);
                        dots.Add(current);
                        collidable = Physics2D.Raycast(current, new Vector2(0, -1), Mathf.Infinity, 1 << LayerMask.NameToLayer("Collidable"));
                    } while (collidable.distance > jump_level && t < simulationTime);

                    float delta = collidable.distance - jump_level;

                    dots[dots.Count - 1] -= new Vector3(0, delta, 0);

                    point2.transform.position = dots[dots.Count - 1];

                    Handles.DrawPolyLine(dots.ToArray());
                }
            }
        }
    }
#endif
}
