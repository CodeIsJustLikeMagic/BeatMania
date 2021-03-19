using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class ColorChange : VibingEntity
{
    public abstract void setColor(int song);

    public override void OnSongChange(int song)
    {
        setColor(song);
    }

    public override void OnBeat(float bps)
    {
    }
}
