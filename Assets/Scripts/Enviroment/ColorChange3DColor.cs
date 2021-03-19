using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange3DColor : ColorChange
{
    [SerializeField]
    private Renderer rend;

    public Color[] colors;
    public override void setColor(int song)
    {
        rend = GetComponent<Renderer>();
        rend.sharedMaterial.SetColor("_Color", colors[song%colors.Length]);
    }
    
}
