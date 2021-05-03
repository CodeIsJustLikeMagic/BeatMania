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
        if (GUILayout.Button("update current background color"))
        {
            background.UpdateColors();
        }
    }


    [MenuItem("Tools/Swap to next background color")]
    private static void swapToNextBackgroundColor()
    {
        FindObjectOfType<Background>().SetNextSprites();
    }
}
