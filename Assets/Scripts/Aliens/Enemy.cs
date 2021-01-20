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
    /// 

    private string[] combo = { "charge", "atk1", "charge", "atk2", "charge", "atk3", "atk4", "atk5","wait", "wait" };
    //  this combo array could be replaced by an array of AttackStructs. That way we can save the dmg of the spell and if enemy can be staggered during it.

    private void Awake()
    {
        maxCombo = combo.Length;
    }

    public Animator enemyAnimator2D;
    public Animator enemyAnimator3D;
    public override void OnBeat(float bps)
    {
        Debug.Log("OnBeat");
        if (CheckAttack())
        {
            Attack();
        }
        else
        {
            enemyAnimator3D.SetTrigger("Wait");
        }
        enemyAnimator3D.SetFloat("Speed", bps);
    }
    //test if player is close enough to attack
    private bool CheckAttack()
    {
        return true;
    }


    //perform attack/animation
    private int combocounter = 0;
    private int maxCombo = 5;
    private void Attack()//set up with simple combo same as player but without being able to miss beats
    {
        Debug.Log("enemy Attack");
        string move = combo[combocounter];
        if (move.Contains("atk"))
        {//perform an attack. animation is dictated by integer number
            enemyAnimator2D.SetTrigger("Attack");
            enemyAnimator2D.SetInteger("AttackNum", int.Parse(move.Substring(3)));

            enemyAnimator3D.SetTrigger("Attack");
        }
        else
        {//either charge or wait

            enemyAnimator2D.SetTrigger(move);
            if(move == "charge")
            {
                enemyAnimator3D.SetTrigger("Charge");
            }
            if(move == "wait")
            {
                enemyAnimator3D.SetTrigger("Wait");
            }
        }
        combocounter = (combocounter + 1) % maxCombo;//sycle though combo array
    }

    public void Stagger()//get staggered by getting hit. 
    {
        combocounter = 0;//rest combo
        enemyAnimator3D.SetTrigger("Dizzy");//play stagger animation as feedback
    }


}
