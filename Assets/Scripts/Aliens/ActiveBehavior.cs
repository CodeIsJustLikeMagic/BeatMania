using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveBehavior : AlienBehavior
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
        //heal player when in range, every 4 beats
        if (PlayerIsInRange())
        {
            if (cd <= 0)
            {
                Heal();
                cd = cooldownHelpAbility;
            }
            else
            {
                cd--;
                enemyAnimator3D.SetTrigger("Wait");
            }
        }
    }

    public int cooldownHelpAbility = 4;
    private int cd = 0;

    private bool PlayerIsInRange()
    {
        return true;
    }

    private void Heal()
    {
        enemyAnimator2D.SetTrigger("Heal");
        enemyAnimator3D.SetTrigger("Heal");
        //heal the player
    }
}
