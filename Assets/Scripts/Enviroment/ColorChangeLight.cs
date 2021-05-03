using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangeLight : ColorChange
{
    [SerializeField]
    private Light light;

    public Color[] colors = { new Color(255, 255, 255, 255), new Color(255, 255, 255, 255), new Color(255, 255, 255, 255) };
    protected override void showColor(int song)
    {
        light = GetComponentInChildren<Light>();
        light.color = colors[song%colors.Length];
    }
}
