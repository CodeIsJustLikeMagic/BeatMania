using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangeSprite : ColorChange
{
    [SerializeField]
    private SpriteRenderer spriteRend;

    public Sprite[] sprites;

    public override void setColor(int song)
    {
        spriteRend = GetComponent<SpriteRenderer>();
        spriteRend.sprite = sprites[song % sprites.Length];
    }

}
