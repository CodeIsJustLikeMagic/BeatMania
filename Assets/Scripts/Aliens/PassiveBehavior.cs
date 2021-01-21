using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveBehavior : AlienBehavior
{
    private Animator enemyAnimator2D;
    private Animator enemyAnimator3D;
    private void Start()
    {
        enemyAnimator2D = gameObject.GetComponent<AlienHandleSongChange>().enemyAnimator2D;
        enemyAnimator3D = gameObject.GetComponent<AlienHandleSongChange>().enemyAnimator3D;
    }

    public override void PerformBehaviorOnBeat(float bps)
    {
        enemyAnimator3D.SetTrigger("Waiting");
    }

}
