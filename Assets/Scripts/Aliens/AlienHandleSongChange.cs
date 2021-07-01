﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienHandleSongChange : VibingEntity
{
    [SerializeField]
    private AlienBehavior[] behaviors; //order of behaviors dictates which berhavior is performed in which song
    
    

    public Animator enemyAnimator2D;
    public Animator enemyAnimator3D;

    private void Awake()
    {
        OnSongChange(0); //set default to be first song
    }
    public delegate void ActionDelegate(float bps);
    ActionDelegate actionMethod;

    public override void OnBeat(float jitter_delay, float bps)
    {
        AlienHandleSongChange.bps = bps;
        enemyAnimator3D.SetFloat("Speed", bps);
        //dictate which Behavior to Perform, based on musik (passive, attack, active)
        CancelInvoke();
        InvokeRepeating("ActionOnBeat",  SongSynchonizeVibing.instance.BeatStart + SongSynchonizeVibing.instance.BeatLength - Time.time, 1 / bps);
        if (SongSynchonizeVibing.instance.BeatStart - Time.time + SongSynchonizeVibing.instance.BeatLength < 0)
        {
            Debug.LogError("AlienHandleSongChange failed to Start Action Invoke beacause Time-BeatStart is negative", this);
        }
    }

    private static float bps;
    private void ActionOnBeat()
    {
        actionMethod(bps);
    }
    

    public override void OnSongChange(int song)
    {
        foreach (var b in behaviors)
        {
            b.enabled = false; // when script is disabled it's update function isnt called anymore. Other functions can still be called.
        }

        behaviors[song % behaviors.Length].enabled = true;
        actionMethod = behaviors[song % behaviors.Length].PerformBehaviorOnBeat;
        GetComponent<AlienHealthBehavior>().OnSongChange(song);
        GetComponent<WalkBehavior>().Stop(); // dont keep walking when song is changed.
    }


}
