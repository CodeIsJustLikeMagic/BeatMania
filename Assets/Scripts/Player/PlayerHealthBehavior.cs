using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthBehavior : BaseHealthBehavior
{
    protected override void HandleCollision(Collider hit)
    {
        if (hit.gameObject.tag == "Enemy")
        {
            
            Health -= hit.gameObject.GetComponent<AttackBehavior>().dmgToPlayer;
            if (Health <= 0.3)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        Debug.Log("Player "+gameObject.name+" is dead now");
    }
}
