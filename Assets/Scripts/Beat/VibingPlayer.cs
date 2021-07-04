using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;

public class VibingPlayer : VibingEntity
{
    private int[] songSpeeds = { 120, 100, 120, 140};
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private ComboAttack combo;

    private void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        combo = gameObject.GetComponent<ComboAttack>();
        if (anim == null)
        {
            anim = gameObject.GetComponentInChildren<Animator>();
        }
    }

    private int stateName;
    public override void OnBeat(float jitter_delay, float bps)
    {
        stateName = anim.GetCurrentAnimatorStateInfo(1).fullPathHash;
        if (stateName == null)
        {
            Debug.Log("stateName is null", this);
        }
        anim.Play(stateName, 0, jitter_delay);
        //restart animationNow

        anim.SetFloat("AnimationSpeed", bps/2);

        combo.SetBps(bps);
    }
}
