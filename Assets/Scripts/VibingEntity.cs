using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class VibingEntity : MonoBehaviour
{
    private void Start()
    {
        
        AnimationOnBeat.instance.AddMyselfToList(this);
    }

    public abstract void OnBeat(float bps);
}
