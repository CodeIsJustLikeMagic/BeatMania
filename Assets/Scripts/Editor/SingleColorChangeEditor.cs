using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(ColorChange), true)]
public class SingleColorChangeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        ColorChange t = (ColorChange)target;
        if (GUILayout.Button("swap to next color"))
        {
            t.setColor();
        }
    }
}
