using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlotPoint))]
public class PlotPointUI : Editor
{
    private static float debug_radius = 0.2f;
    private void OnSceneGUI()
    {
        PlotPoint point = (PlotPoint)target;
        Handles.color = Color.green;

        Handles.DrawSolidDisc(point.transform.position, new Vector3(0, 0, 1), debug_radius);
        Handles.Label(point.transform.position - new Vector3(0, debug_radius, 0), point.id.ToString());
    }
}
