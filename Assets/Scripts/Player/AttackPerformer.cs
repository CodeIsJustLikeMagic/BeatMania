using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Permissions;
using UnityEngine;

public class AttackPerformer : VibingEntity // sits on Enemy
{
    
    // !!! make sure this script is on the same gameObject as your trigger collider. Only then will OnTriggerEnter be called. 
    [SerializeField] private Animator anim = null;

    public string dmg_tag = "Player";
    public string entityName = "Enemy";
    // Start is called before the first frame update
    private new void Start()
    {
        base.Start();
        if (anim == null)
        {
            anim = GetComponentInChildren<Animator>();
        }
    }

    private float bps = 0;

    public override void OnBeat(float jitter_delay, float bps)
    {
        this.bps = bps;
        anim.SetFloat("Speed", bps);
    }

    //remember stats of the last perform call
    private float dmg;
    private bool stagger;

    public void Perform(string trigger, float dmg, bool stagger, string target_tag, bool resync = false)
    {
        //Debug.Log("Attack Performer perfrom, targets: "+dmg_tag);
        dmg_tag = target_tag;
        if (trigger != "")
        {            
            this.dmg = dmg;
            this.stagger = stagger;
            anim.SetTrigger(trigger);
        }

        if (resync)
        {
            int stateName = anim.GetCurrentAnimatorStateInfo(0).fullPathHash;
            anim.Play(stateName, 0, SongSynchonizeVibing.Instance.GetJitterOffset()); //*bps);
        }
        //Debug.Log("Attack Performer perform done. InBeat? "+BeatChecker.Instance.IsInBeat(), this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(dmg_tag))
        {
            BaseHealthBehavior hp = other.gameObject.GetComponent<BaseHealthBehavior>();
            hp.ApplyDamage(dmg, stagger, transform.position,entityName);
            
        }
    }
}
