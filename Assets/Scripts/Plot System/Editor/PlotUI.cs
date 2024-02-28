using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Plot))]
public class PlotUI : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Plot plot = (Plot)target;


        if (GUILayout.Button("Add Point"))
        {
            plot.AddPoint();
        }

        if (GUILayout.Button("Delete Point"))
        {
            plot.DeletePoint();
        }
    }
}
