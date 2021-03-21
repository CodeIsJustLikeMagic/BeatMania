using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class ColorChange : VibingEntity
{
    public abstract void setColor(int song);

    private int s = 0;
    public void setColor()
    {
        s += 1;
        setColor(s);
    }

    public override void OnSongChange(int song)
    {
        setColor(song);
    }

    public override void OnBeat(float bps)
    {
    }
}
