using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class AttackPerformer : MonoBehaviour // sits on Enemy
{
    
    // !!! make sure this script is on the same gameObject as your trigger collider. Only then will OnTriggerEnter be called. 
    [SerializeField]
    private Animator anim = null;

    public string dmg_tag = "Player";
    // Start is called before the first frame update
    void Start()
    {
        if (anim == null)
        {
            anim = GetComponentInChildren<Animator>();
        }
    }

    //remember stats of the last perform call
    private float dmg;
    private bool stagger;

    public void SetBsp(float bps)
    {
        anim.SetFloat("Speed", bps);
    }
    public void Perform(string trigger, float dmg, bool stagger, string target_tag)
    {
        dmg_tag = target_tag;
        if (trigger != "")
        {
            anim.SetTrigger(trigger);
            this.dmg = dmg;
            this.stagger = stagger;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(dmg_tag))
        {
            BaseHealthBehavior hp = other.gameObject.GetComponent<BaseHealthBehavior>();
            hp.ApplyDamage(dmg, stagger, transform.position);
        }
    }
}
