using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBehavior : BaseHealthBehavior
{
    protected override void HandleCollision(Collider hit)
    {
        if (hit.gameObject.tag == "Projectile")
        {
            
            Health -= 1f;
            Debug.Log("Collision with Enemy Health: "+Health);
            if (Health <= 0.3)
            {
                Debug.Log("Die");
            }
        }
    }
}
