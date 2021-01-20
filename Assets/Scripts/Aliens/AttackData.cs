using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct AttackData
{
    public float damage;
    public int animationnum;

    public AttackData(float damage, int animationnum)
    {
        this.damage = damage;
        this.animationnum = animationnum;
    }
}
