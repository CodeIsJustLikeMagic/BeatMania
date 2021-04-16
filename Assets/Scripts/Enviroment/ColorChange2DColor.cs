using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange2DColor : ColorChange
{
    [SerializeField]
    private SpriteRenderer sprite;
    [SerializeField]
    private Color[] colors = { new Color(255, 255, 255, 255), new Color(255, 255, 255, 255), new Color(255, 255, 255, 255) };

    protected override void showColor(int song)
    {
        sprite = GetComponent<SpriteRenderer>();
        if(sprite == null)
        {
            sprite = GetComponentInChildren<SpriteRenderer>();
            if(sprite == null)
            {
                Debug.LogError(gameObject.name + " Element is missing its spriteRenderer");
            }
        }
        if(colors.Length == 0)
        {
            return;
        }
        sprite.color = colors[song % colors.Length];
    }
}
