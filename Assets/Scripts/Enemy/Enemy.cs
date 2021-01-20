using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : VibingEntity
{
    //Enemy needs:
    /// <summary>
    /// hp, death
    /// check for attack, attack with beat, choose attack based on combo
    /// get staggered
    /// 
    /// </summary>

    public Animator enemyAnimator2D;
    public Animator enemyAnimator3D;
    public override void OnBeat(float bps)
    {
        Debug.Log("OnBeat");
        if (CheckAttack())
        {
            Attack();
        }
    }
    //test if player is close enough to attack
    private bool CheckAttack()
    {
        return true;
    }


    private int combocounter = 0;
    int maxCombo = 5;

    private void Attack()//set up with simple combo same as player but without being able to miss beats
    {
        Debug.Log("enemy Attack");
        enemyAnimator2D.SetTrigger("TAN" + (combocounter + 1)); // Test Attack Normal 1-5
        enemyAnimator3D.SetTrigger("Attack");
        enemyAnimator3D.SetInteger("AttackNum", combocounter % 2);
        combocounter = (combocounter + 1) % maxCombo;
    }

    public void Stagger()//get staggered by getting hit. 
    {
        combocounter = 0;//rest combo
        enemyAnimator3D.SetTrigger("Dizzy");//play stagger animation as feedback
    }
}
