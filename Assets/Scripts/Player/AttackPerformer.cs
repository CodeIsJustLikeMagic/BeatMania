﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Permissions;
using UnityEngine;

public class AttackPerformer : VibingEntity // sits on Enemy
{
    
    // !!! make sure this script is on the same gameObject as your trigger collider. Only then will OnTriggerEnter be called. 
    [SerializeField] private Animator anim = null;

    public string dmg_tag = "Player";
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        if (anim == null)
        {
            anim = GetComponentInChildren<Animator>();
        }
    }

    public override void OnBeat(float jitter_delay, float bps)
    {
        Debug.Log("AttackPerformer On Beat");
        anim.SetFloat("Speed", bps);
    }

    //remember stats of the last perform call
    private float dmg;
    private bool stagger;

    public void Perform(string trigger, float dmg, bool stagger, string target_tag)
    {
        Debug.Log("Perform action performer "+trigger, this);
        dmg_tag = target_tag;
        if (trigger == "Attack")
        {
            anim.SetTrigger(trigger);
            this.dmg = dmg;
            this.stagger = stagger;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(dmg_tag))
        {
            BaseHealthBehavior hp = other.gameObject.GetComponent<BaseHealthBehavior>();
            hp.ApplyDamage(dmg, stagger, transform.position);
            
        }
    }
}
