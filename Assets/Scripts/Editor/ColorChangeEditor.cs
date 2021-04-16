using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(ColorChange), true), CanEditMultipleObjects]
public class ColorChangeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        Object[] t = targets;
        if (GUILayout.Button("swap to next color"))
        {
            foreach(var script in t)
            {
                ((ColorChange)script).setColor();
            }
        }
        if (GUILayout.Button("update current color"))
        {
            foreach (var script in t)
            {
                ((ColorChange)script).updateColor();
            }
        }
        if (GUILayout.Button("swap everything to next color"))
        {
            foreach (var script in t)
            {
                FindObjectOfType<Background>().SetNextSprites();
            }
        }
    }
}
