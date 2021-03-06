﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

/// <summary>
/// Handles attack pattern and animations for an agressive alien.
/// </summary>
public class AttackBehavior : AlienBehavior
{
    [SerializeField]
    private string[] combo = { "Charge", "Attack", "Charge", "Attack", "Charge", "Attack", "Attack", "Attack", "Wait", "Wait" };
    
    //  this combo array could be replaced by an array of AttackStructs. That way we can save the dmg of the spell and if enemy can be staggered during it.
    public float dmgToPlayer = 1f;
    private Animator enemyAnimator3D;
    [SerializeField] private bool only3D = true;

    [SerializeField]
    private float attack_range = 2.4f;

    private WalkBehavior _walkBehavior;
    private AttackPerformer attackPerformer;
    private void Start()
    {
        enemyAnimator3D = gameObject.GetComponent<AlienHandleSongChange>().enemyAnimator3D;
        _walkBehavior = gameObject.GetComponent<WalkBehavior>();
        attackPerformer = gameObject.GetComponentInChildren<AttackPerformer>();
    }
    
    public override void PerformBehaviorOnBeat(float bps)
    {
        WalkState s = _walkBehavior.CheckForEnemyInRange(bps, attack_range, true, true);
        if (s == WalkState.See_And_In_Range && !skip)
        {
            Attack();
        }

        if (skip)
        {
            skip = false;
        }
    }

    private void Awake()
    {
        maxCombo = combo.Length;
    }

    //perform attack/animation
    private int combocounter = 0;
    private int maxCombo = 5;
    private bool skip; // when you want to stop alien from performing behavior for one beat.
    //used when getting hit or dizzy to stop alien from interrupting its "gethit" animation.
    //skip could be changed to an int so you can make alien skip the next X beats.
    //when you want it to get staggered from mutliple beats or something.
    private void Attack()//set up with simple combo same as player but without being able to miss beats
    {
        string move = combo[combocounter];
        if (move != "Charge" && move != "Wait")
        {
            _walkBehavior.KeepMoving();
        }
        if (!only3D)
        {
            enemyAnimator3D.SetTrigger(move);
            attackPerformer.Perform(move, 1, false, "Player");
        }
        else
        {
            attackPerformer.Perform(move, 1, false, "Player");
        }
        
        
        combocounter = (combocounter + 1) % maxCombo;//cycle though combo array
    }

    public void Stagger()//get staggered by getting hit. 
    {
        combocounter = 0;//rest combo
        enemyAnimator3D.SetTrigger("Dizzy");//play stagger animation as feedback
    }

    public void GetHit()
    {
        //enemyAnimator3D.SetTrigger("GetHit");
        throw new NotImplementedException();
    }
}
