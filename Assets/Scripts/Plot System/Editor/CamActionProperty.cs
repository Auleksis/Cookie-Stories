using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(CamActionDescriptor))]
public class CamActionProperty : PropertyDrawer
{
    Rect text_rect;
    private int GetEnumValueIndex(CamActionType type)
    {
        return Array.IndexOf(Enum.GetValues(typeof(CamActionType)), type);
    }

    private CamActionType GetEnumValueFromIndex(int index)
    {
        return (CamActionType)Enum.GetValues(typeof(CamActionType)).GetValue(index);
    }

    private void ShowTypeField(Rect position, SerializedProperty property)
    {

    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //EditorGUI.LabelField(position, label, new GUIContent("TODO"));

        EditorGUI.BeginProperty(position, label, property);
        Rect rectFoldout = new Rect(position.min.x, position.min.y, position.size.x, EditorGUIUtility.singleLineHeight);

        property.isExpanded = EditorGUI.Foldout(rectFoldout, property.isExpanded, label);
        int lines = 1;

        if (property.isExpanded)
        {
            Rect rectType = new Rect(position.min.x, position.min.y + lines++ * EditorGUIUtility.singleLineHeight, position.size.x, EditorGUIUtility.singleLineHeight);
            SerializedProperty propType = property.FindPropertyRelative("type");
            EditorGUI.PropertyField(rectType, property.FindPropertyRelative("type"));

            ShowTypeField(rectType, propType);

            CamActionType chosen = GetEnumValueFromIndex(propType.enumValueIndex);

            if (chosen == CamActionType.TRANSLATE)
            {
                Rect lookToRect = new Rect(position.min.x, position.min.y + lines++ * EditorGUIUtility.singleLineHeight, position.size.x, EditorGUIUtility.singleLineHeight);
                SerializedProperty lookToProp = property.FindPropertyRelative("lookTo");
                EditorGUI.PropertyField(lookToRect, lookToProp);
            }

            if (chosen == CamActionType.WAIT)
            {
                Rect secondsRect = new Rect(position.min.x, position.min.y + lines++ * EditorGUIUtility.singleLineHeight, position.size.x, EditorGUIUtility.singleLineHeight);
                SerializedProperty secondsProp = property.FindPropertyRelative("waitForSeconds");
                secondsProp.floatValue = EditorGUI.FloatField(secondsRect, secondsProp.floatValue);
            }

            Rect dependedWithPlotRect = new Rect(position.min.x, position.min.y + lines++ * EditorGUIUtility.singleLineHeight, position.size.x, EditorGUIUtility.singleLineHeight);
            SerializedProperty dependedWithPlotProp = property.FindPropertyRelative("dependedWith");
            EditorGUI.PropertyField(dependedWithPlotRect, dependedWithPlotProp);

            Rect keyPointRect = new Rect(position.min.x, position.min.y + lines++ * EditorGUIUtility.singleLineHeight, position.size.x, EditorGUIUtility.singleLineHeight);
            SerializedProperty keyPointProp = property.FindPropertyRelative("camActionWillStartAfterPoint");
            keyPointProp.intValue = EditorGUI.IntField(keyPointRect, keyPointProp.intValue);
        }

        //Debug.Log(property.enumValueIndex);

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        int totalLines = 2;

        if (property.isExpanded)
        {
            totalLines++;
            SerializedProperty propType = property.FindPropertyRelative("type");
            switch (GetEnumValueFromIndex(propType.enumValueIndex))
            {
                default:
                    totalLines = 6;
                    break;
            }
        }
        return EditorGUIUtility.singleLineHeight * totalLines + EditorGUIUtility.standardVerticalSpacing * (totalLines - 1);
    }
}
