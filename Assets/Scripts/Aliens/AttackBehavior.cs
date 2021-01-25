using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles attack pattern and animations for an agressive alien.
/// </summary>
public class AttackBehavior : AlienBehavior
{
    [SerializeField]
    private string[] combo = { "charge", "atk1", "charge", "atk2", "charge", "atk3", "atk4", "atk5", "wait", "wait" };
    //  this combo array could be replaced by an array of AttackStructs. That way we can save the dmg of the spell and if enemy can be staggered during it.

    private Animator enemyAnimator2D;
    private Animator enemyAnimator3D;
    private void Start()
    {
        enemyAnimator2D = gameObject.GetComponent<AlienHandleSongChange>().enemyAnimator2D;
        enemyAnimator3D = gameObject.GetComponent<AlienHandleSongChange>().enemyAnimator3D;
    }
    
    public override void PerformBehaviorOnBeat(float bps)
    {
        if (CheckAttack())
        {
            Attack();
        }
        else
        {
            enemyAnimator3D.SetTrigger("Wait");
        }
    }

    //test if player is close enough to attack
    private bool CheckAttack()
    {
        return true;
    }

    private void Awake()
    {
        maxCombo = combo.Length;
    }

    //perform attack/animation
    private int combocounter = 0;
    private int maxCombo = 5;
    private void Attack()//set up with simple combo same as player but without being able to miss beats
    {
        string move = combo[combocounter];
        if (move.Contains("atk"))
        {//perform an attack. animation is dictated by integer number
            enemyAnimator2D.SetTrigger("Attack");
            enemyAnimator2D.SetInteger("AttackNum", int.Parse(move.Substring(3)));

            enemyAnimator3D.SetTrigger("Attack");
        }
        else
        {//either charge or wait

            //enemyAnimator2D.SetTrigger(move);
            if (move == "charge")
            {
                enemyAnimator3D.SetTrigger("Charge");
            }
            if (move == "wait")
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
