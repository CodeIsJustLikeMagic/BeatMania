using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMoving : AlienBehavior
{

    [Tooltip("Understands: Up, Down, GoUp, GoDown, Invisible, Visible")]
    [SerializeField]
    private string[] combo = { "Up", "Up","Down", "Down", "Invisible", "Visible"}; //example combo

    [SerializeField] private int move_every_x_beats = 1;
    private int beat_num = 1;
    private Animator animator;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private int combocounter = 0;
    public string debug = "";
    public override void PerformBehaviorOnBeat(float bps)
    {
        if (beat_num < move_every_x_beats)
        {
            beat_num++;
            return;
        }

        beat_num = 1;
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
        if (animator != null && animator.isActiveAndEnabled)
        {
            animator.SetFloat("Speed", bps);
            string move = combo[combocounter];
            debug = move;
            animator.SetTrigger(move);
            combocounter = (combocounter + 1) % combo.Length;// cycle though combo array
        }
    }

}
