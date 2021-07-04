using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnTouch : MonoBehaviour
{
    public string dmg_tag = "Player";
    [SerializeField] private float dmg = 1;
    [SerializeField] private bool staggers = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(dmg_tag))
        {
            BaseHealthBehavior hp = other.gameObject.GetComponent<BaseHealthBehavior>();
            hp.ApplyDamage(dmg, staggers, transform.position, forceMulti: 10f); //apply damage to the player on touch
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        OnTriggerEnter(collision.collider);
    }
}
