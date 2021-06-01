using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangeLight : ColorChange
{
    [SerializeField]
    private Light myLight;

    public Color[] colors = { new Color(255, 255, 255, 255), new Color(255, 255, 255, 255), new Color(255, 255, 255, 255) };
    protected override void showColor(int song)
    {
        myLight = GetComponentInChildren<Light>();
        myLight.color = colors[song%colors.Length];
    }
}
