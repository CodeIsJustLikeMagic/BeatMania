using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class ColorChange : VibingEntity
{
    public void setColor(int song)
    {
        s = song;
        showColor(s);
    }

    protected abstract void showColor(int song);

    private int s = 0;
    public void setColor()
    {
        s += 1;
        setColor(s);
    }
    public void updateColor()
    {
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
