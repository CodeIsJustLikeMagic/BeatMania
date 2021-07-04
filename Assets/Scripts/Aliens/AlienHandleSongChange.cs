using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienHandleSongChange : VibingEntity
{
    [SerializeField]
    private AlienBehavior[] behaviors; //order of behaviors dictates which berhavior is performed in which song
    
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
    
    [NonSerialized] public bool is_dead = false; // Alien gets supressed before it dies So it doesnt interrupt its attack animation.

    [NonSerialized] public int skip = 0;
    //essentally used when skip isnt enough.
    private static float bps;
    private void ActionOnBeat()
    {
        if (is_dead) // 2 ways to stop alien from acting. Either it is dead and needs to be revived to act again
        {
            return;
        }

        if (skip > 0) // or it skips a number of beats and starts to act again. For stagger
        {
            skip--;
            return;
        }
        
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
