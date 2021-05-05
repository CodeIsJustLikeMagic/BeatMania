using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class AttackPerformer : MonoBehaviour
{
    [SerializeField]
    public Animator anim;

    [SerializeField] private string dmg_entities_with_tag = "Player";
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    //remember stats of the last perform call
    private float dmg;
    private bool stagger;

    public void SetBsp(float bps)
    {
        anim.SetFloat("Speed", bps);
    }
    public void Perform(string trigger, float dmg, bool stagger)
    {
        if (trigger != "")
        {
            anim.SetTrigger(trigger);
            this.dmg = dmg;
            this.stagger = stagger;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == dmg_entities_with_tag)
        {
            //Debug.Log("hit enitity with tag "+ dmg_entities_with_tag, this);
            BaseHealthBehavior hp = other.gameObject.GetComponent<BaseHealthBehavior>();
            hp.ApplyDamage(dmg, stagger, transform.position);
        }
    }
}
