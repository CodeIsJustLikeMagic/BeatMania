using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpritesLightingMaterial : MonoBehaviour
{
    [MenuItem("Tools/Update Sprites to be Illuminatable")]
    private static void SetMaterial()
    {
        SpriteRenderer[] renderers = FindObjectsOfType<SpriteRenderer>();
        foreach (SpriteRenderer renderer in renderers)
        {
            renderer.material = Resources.Load<Material>("SpritesDiffuse");
        }
    }
}
