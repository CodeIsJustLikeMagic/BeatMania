﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange2DSprite : ColorChange
{
    [SerializeField]
    private SpriteRenderer spriteRend;

    public Sprite[] sprites;

    protected override void showColor(int song)
    {
        spriteRend = GetComponent<SpriteRenderer>();
        spriteRend.sprite = sprites[song % sprites.Length];
    }
}
