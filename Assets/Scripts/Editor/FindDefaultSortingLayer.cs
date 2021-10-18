using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FindDefaultSortingLayer : Editor
{
    [MenuItem("Tools/Find Sprite Renderers with Default Layer")]
    private static void createColorChange()
    {
        var renderers = FindObjectsOfType<SpriteRenderer>();
        List<Object> objects = new List<Object>();
        foreach (SpriteRenderer r in renderers)
        {
            if (r.sortingLayerName == "Default")
            {
                objects.Add(r.gameObject);
            }
        }

        Selection.objects = objects.ToArray();
    }
}
