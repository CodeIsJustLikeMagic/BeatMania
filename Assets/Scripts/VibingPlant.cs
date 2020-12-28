using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibingPlant : VibingEntity
{
    public Animator anim;
    public override void OnBeat(float bps)
    {
        anim.SetTrigger("beat");
        Debug.Log(gameObject.name + " setAnimLength ");
        anim.SetFloat("animSpeed", bps);
        //Debug.Log("anim speed is " + anim.speed);
        //restart animationNow
    }
}
