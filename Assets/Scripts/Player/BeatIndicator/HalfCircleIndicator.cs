﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfCircleIndicator : VibingEntity
{
    public float start_anim_at_normalized_Time = 0.0f;
    public float animationSpeedMultiplier = 1;
    
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

    private int stateName;
    public override void OnBeat(float bps)
    {
        stateName = anim.GetCurrentAnimatorStateInfo(0).fullPathHash;
        anim.Play(stateName, 0, start_anim_at_normalized_Time);
        //restart animationNow
        
        anim.SetFloat("Speed", animationSpeedMultiplier* bps);
    }
}
