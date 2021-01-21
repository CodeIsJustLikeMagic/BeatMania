using System.Collections;
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

    public override void OnBeat(float bps)
    {
        //dictate which Behavior to Perform, based on musik (passive, attack, active)
        actionMethod(bps);
    }

    public override void OnSongChange(int song)
    {
        actionMethod = behaviors[song % behaviors.Length].PerformBehaviorOnBeat;
    }


}
