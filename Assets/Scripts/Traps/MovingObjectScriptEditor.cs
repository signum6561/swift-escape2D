using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MovingObject))]
public class MovingObjectScriptEditor : Editor
{
    MovingObject movingTrap;
    SerializedObject movingTrapObject;
    SerializedProperty movingPoints;
    private void OnEnable()
    {
        movingTrap = (MovingObject)target;
        movingTrapObject = new SerializedObject(target);
        movingPoints = movingTrapObject.FindProperty("MovePoints");
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        movingTrapObject.Update();
        if (GUILayout.Button("Create moving Point"))
        {
            movingPoints.arraySize++;
            SerializedProperty newPoint = movingPoints.GetArrayElementAtIndex(movingPoints.arraySize - 1);
            newPoint.vector2Value = movingTrap.transform.position;

        }
        movingTrapObject.ApplyModifiedProperties();
    }

}
