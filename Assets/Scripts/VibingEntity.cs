using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//VibingEntities add themselves to AnimationOnBeat, which manages timing and beatchange synchronisation with the clock
abstract public class VibingEntity : MonoBehaviour
{
    private void Start()
    {
        
        AnimationOnBeat.instance.AddMyselfToList(this);
    }

    public abstract void OnBeat(float bps);
}
