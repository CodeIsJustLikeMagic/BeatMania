using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine.U2D;

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

    [MenuItem("Tools/Create Color Change 2D With current Color")]
    private static void createColorChange()
    {
        SpriteRenderer[] currentSelection = Selection.GetFiltered<SpriteRenderer>(SelectionMode.Deep);
        Debug.Log("currentSelection length "+ currentSelection.Length);
        foreach (SpriteRenderer r in currentSelection)
        {
            if (r.GetComponent<ColorChange2DColor>() == null)
            {
                r.gameObject.AddComponent<ColorChange2DColor>();
                ColorChange2DColor c = r.GetComponent<ColorChange2DColor>();
                Color color = r.GetComponent<SpriteRenderer>().color;
                c.setColor(color, 0);
                c.setColor(color, 1);
                c.setColor(color, 2);
            }
            ColorChange2DColor c2 = r.GetComponent<ColorChange2DColor>();
            Color color2 = r.GetComponent<SpriteRenderer>().color;
            c2.setColor(color2, FindObjectOfType<Background>().getSong());
        }
    }
}
