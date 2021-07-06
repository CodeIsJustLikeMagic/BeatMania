using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealBehavior : AlienBehavior
{
    [SerializeField] private float heal_by = 1;
    [SerializeField] int HealEveryXBeats = 4;
    private HealPerformer _healPerformer;
    private Animator enemyAnimator3D;
    private void Start()
    {
        _healPerformer = gameObject.GetComponentInChildren<HealPerformer>();
        if (_healPerformer == null)
        {
            Debug.LogError("Enemy has no heal performer. It is sad.", this);
        }
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
                cd = HealEveryXBeats;
            }
            else
            {
                cd--;
                enemyAnimator3D.SetTrigger("Wait");
            }
        }
    }


    private int cd = 0;

    private bool PlayerIsInRange()
    {
        return true;
    }

    private void Heal()
    {
        _healPerformer.Heal("Heal", heal_by, "Player");
        enemyAnimator3D.SetTrigger("Heal");
        //heal the player
    }
}
