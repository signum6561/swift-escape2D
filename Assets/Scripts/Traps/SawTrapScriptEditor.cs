using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SawTrap))]
public class SawTrapScriptEditor : Editor
{
    SawTrap sawTrap;
    SerializedObject sawTrapObject;
    SerializedProperty sawPoints;
    private void OnEnable()
    {
        sawTrap = (SawTrap)target;
        sawTrapObject = new SerializedObject(target);
        sawPoints = sawTrapObject.FindProperty("SawPoints");
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        sawTrapObject.Update();
        if (GUILayout.Button("Create Saw Point"))
        {
            sawPoints.arraySize++;
            SerializedProperty newPoint = sawPoints.GetArrayElementAtIndex(sawPoints.arraySize - 1);
            newPoint.vector2Value = sawTrap.transform.position;

        }
        sawTrapObject.ApplyModifiedProperties();
    }

}
