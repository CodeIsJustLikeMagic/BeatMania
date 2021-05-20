using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformHandleSongChange : VibingEntity
{
    [SerializeField]
    private AlienBehavior[] behaviors; //order of behaviors dictates which berhavior is performed in which song
    
    private Animator animator;

    private void Awake()
    {
        OnSongChange(0); //set default to be first song
        animator = GetComponentInChildren<Animator>();
    }
    public delegate void ActionDelegate(float bps);
    ActionDelegate actionMethod;

    public override void OnBeat(float jitter_delay, float bps)
    {
        PlatformHandleSongChange.bps = bps;
        //dictate which Behavior to Perform, based on musik (passive, attack, active)
        CancelInvoke();
        InvokeRepeating("ActionOnBeat", SongSynchonizeVibing.instance.BeatStart+SongSynchonizeVibing.instance.BeatLength - Time.time, 1/bps);
        actionMethod(bps);
    }

    private static float bps;

    private void ActionOnBeat()
    {
        actionMethod(bps);
    }

    public override void OnSongChange(int song)
    {
        AlienBehavior behavior = behaviors[song % behaviors.Length];
        if(behavior == null)
        {
            actionMethod = DoNothing;
        }
        else
        {
            actionMethod = behaviors[song % behaviors.Length].PerformBehaviorOnBeat;
        }
    }

    public void DoNothing(float bps){
        animator.SetTrigger("Default");
    }
}
