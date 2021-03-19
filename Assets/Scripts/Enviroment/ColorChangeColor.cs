using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangeColor : ColorChange
{
    [SerializeField]
    private SpriteRenderer sprite;

    public Color[] colors;

    public override void setColor(int song)
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = colors[song % colors.Length];
    }
}
