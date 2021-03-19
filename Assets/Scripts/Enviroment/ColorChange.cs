using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : VibingEntity
{
    [SerializeField]
    private SpriteRenderer sprite;

    public Color[] colors;

    public override void OnBeat(float bps)
    {
        
    }

    // Start is called before the first frame update

    public void setColor(int song)
    {

        sprite = GetComponent<SpriteRenderer>();
        sprite.color = colors[song % colors.Length];
    }

    public override void OnSongChange(int song)
    {
        setColor(song);
    }
}
