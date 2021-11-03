using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseHealthBehavior : MonoBehaviour
{
    public abstract void ApplyDamage(float dmg, bool stagger, Vector3 attack_pos, string attacked_by_entity,float forceMulti = 4f);

    public abstract void ApplyHeal(float dmg, string healed_by_entity);
    public abstract void AddForce(Vector3 position, float forceMulti);
}
