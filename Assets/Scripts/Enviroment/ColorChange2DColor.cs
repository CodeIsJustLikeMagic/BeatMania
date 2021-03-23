using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange2DColor : ColorChange
{
    [SerializeField]
    private SpriteRenderer sprite;
    [SerializeField]
    private Color[] colors;

    protected override void showColor(int song)
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = colors[song % colors.Length];
    }
}
