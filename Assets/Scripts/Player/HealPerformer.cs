using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPerformer : VibingEntity
{
    // !!! make sure this script is on the same gameObject as your trigger collider. Only then will OnTriggerEnter be called. 
    [SerializeField] private Animator anim = null;

    public string heal_tag = "Player";
    [Space(10)]
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

    public override void OnBeat(float jitter_delay, float bps)
    {
        anim.SetFloat("Speed", bps);
    }

    private float heal_amount;
    public void Heal(string trigger, float health_amount, string target_tag)
    {
        heal_tag = target_tag;
        if (trigger != "")
        {
            anim.SetTrigger(trigger);
            this.heal_amount = health_amount;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(heal_tag))
        {
            BaseHealthBehavior hp = other.gameObject.GetComponent<BaseHealthBehavior>();
            hp.ApplyHeal(heal_amount,entityName);
            
        }
    }

}
