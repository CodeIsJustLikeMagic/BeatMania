﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibingPlant : VibingEntity
{
    [SerializeField]
    private Animator anim;
    private void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        if(anim == null)
        {
            anim = gameObject.GetComponentInChildren<Animator>();
        }
    }
    public override void OnBeat(float bps)
    {
        //Debug.Log("beat");
        //anim.SetTrigger("beat");
        //Debug.Log(gameObject.name + " setAnimLength ");
        anim.SetFloat("Speed", bps);
        //Debug.Log("anim speed is " + anim.speed);
        //restart animationNow
    }
}
