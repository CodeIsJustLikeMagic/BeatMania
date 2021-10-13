using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// VibingEntities add themselves to AnimationOnBeat, which manages timing and beatchange synchronisation with the clock.
/// When extending Vibing Entity make sure to not override Start.
/// </summary>

abstract public class VibingEntity : MonoBehaviour
{
    protected void Start()//needs to be start because AnimationOnBeat gets its instance in Awake
    {
        
        SongSynchonizeVibing.Instance.AddMyselfToList(this);
    }

    public abstract void OnBeat(float jitter_delay, float bps);

    public virtual void OnSongChange(int song) { }

    protected virtual void OnDestroy()
    {
        //Debug.Log("call removefrom list");
        //SongSynchonizeVibing.Instance.RemoveFromList(this);
    }
}
