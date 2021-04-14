using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : AlienBehavior
{

    [Tooltip("Understands: Up, Down, GoUp, GoDown, Invisible, Visible")]
    [SerializeField]
    private string[] combo = { "Up", "Up","Down", "Down", "Invisible", "Visible"}; //example combo
    
    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private int combocounter = 0;
    public override void PerformBehaviorOnBeat(float bps)
    {
        animator.SetFloat("Speed", bps);
        string move = combo[combocounter];
        animator.SetTrigger(move);
        combocounter = (combocounter + 1) % combo.Length;// cycle though combo array
    }
}
