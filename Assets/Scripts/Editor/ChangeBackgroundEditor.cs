using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(Background))]
public class ChangeBackgroundEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        Background background = (Background)target;
        if (GUILayout.Button("swap to next background color"))
        {
            
            background.SetNextSprites();
        }
        if (GUILayout.Button("load white sprites"))
        {

            background.SetWhiteSprites();
        }
    }
}
