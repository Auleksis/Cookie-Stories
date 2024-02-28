using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

[CustomPropertyDrawer(typeof(ActionDescriptor))]
public class ActionProperty : PropertyDrawer
{
    Rect text_rect;
    private int GetEnumValueIndex(ActionType type)
    {
        return Array.IndexOf(Enum.GetValues(typeof(ActionType)), type);
    }

    private ActionType GetEnumValueFromIndex(int index)
    {
        return (ActionType)Enum.GetValues(typeof(ActionType)).GetValue(index);
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

            ActionType chosen = GetEnumValueFromIndex(propType.enumValueIndex);

            Rect pointRect = new Rect(position.min.x, position.min.y + lines++ * EditorGUIUtility.singleLineHeight, position.size.x, EditorGUIUtility.singleLineHeight);
            SerializedProperty propPoint = property.FindPropertyRelative("plotPoint");
            EditorGUI.PropertyField(pointRect, propPoint);

            if (chosen == ActionType.JUMP)
            {
                Rect rightRect = new Rect(position.min.x, position.min.y + lines++ * EditorGUIUtility.singleLineHeight, position.size.x, EditorGUIUtility.singleLineHeight);
                SerializedProperty right = property.FindPropertyRelative("right");
                EditorGUI.PropertyField(rightRect, right);
            }

            
            if (chosen == ActionType.SAY)
            {
                Rect stopRect = new Rect(position.min.x, position.min.y + lines++ * EditorGUIUtility.singleLineHeight, position.size.x, EditorGUIUtility.singleLineHeight);
                SerializedProperty stopBool = property.FindPropertyRelative("stopUntillAllTextIsShown");
                EditorGUI.PropertyField(stopRect, stopBool);

                Rect playerStopRect = new Rect(position.min.x, position.min.y + lines++ * EditorGUIUtility.singleLineHeight, position.size.x, EditorGUIUtility.singleLineHeight);
                SerializedProperty playerStopBool = property.FindPropertyRelative("playerStop");
                EditorGUI.PropertyField(playerStopRect, playerStopBool);

                var str = property.FindPropertyRelative("text");
                string[] arr = str.stringValue.Split('\n');
                Rect rectText = new Rect(position.min.x, position.min.y + lines++ * EditorGUIUtility.singleLineHeight, position.size.x, EditorGUIUtility.singleLineHeight * arr.Length);
                text_rect = rectText;
                str.stringValue = EditorGUI.TextArea(rectText, str.stringValue);
            }

            if(chosen == ActionType.START_MOVEMENT || chosen == ActionType.STOP_MOVEMENT || chosen == ActionType.SPAWN)
            {
                Rect unitRect = new Rect(position.min.x, position.min.y + lines++ * EditorGUIUtility.singleLineHeight, position.size.x, EditorGUIUtility.singleLineHeight);
                SerializedProperty unitProp = property.FindPropertyRelative("unit");
                EditorGUI.PropertyField(unitRect, unitProp);
            }

            if(chosen == ActionType.WAIT)
            {
                Rect secondsRect = new Rect(position.min.x, position.min.y + lines++ * EditorGUIUtility.singleLineHeight, position.size.x, EditorGUIUtility.singleLineHeight);
                SerializedProperty secondsProp = property.FindPropertyRelative("waitForSeconds");
                secondsProp.floatValue = EditorGUI.FloatField(secondsRect, secondsProp.floatValue);
            }

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
                case ActionType.SAY:
                    totalLines = 6 + (int)(text_rect.height / EditorGUIUtility.singleLineHeight);
                    break;
                case ActionType.START_MOVEMENT:
                case ActionType.STOP_MOVEMENT:
                case ActionType.SPAWN:
                case ActionType.JUMP:
                case ActionType.WAIT:
                    totalLines = 4;
                    break;
            }
        }
        return EditorGUIUtility.singleLineHeight * totalLines + EditorGUIUtility.standardVerticalSpacing * (totalLines - 1);
    }
}
