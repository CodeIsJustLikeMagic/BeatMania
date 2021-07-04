using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnTouch : MonoBehaviour
{
    public string dmg_tag = "Player";
    [SerializeField] private float dmg = 1;
    [SerializeField] private bool staggers = false;
    [SerializeField] private float force = 10f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(dmg_tag))
        {
            BaseHealthBehavior hp = other.gameObject.GetComponent<BaseHealthBehavior>();
            if (dmg > 0)
            {
                hp.ApplyDamage(dmg, staggers, transform.position, forceMulti: force); //apply damage to the player on touch
            }
            else
            {
                hp.AddForce(transform.position, force);
            }

        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        OnTriggerEnter(collision.collider);
    }
}
