using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseHealthBehavior : MonoBehaviour
{
    public abstract void ApplyDamage(float dmg, bool stagger, Vector3 attack_pos, float forceMulti = 4f);

    public abstract void ApplyHeal(float dmg);
    public abstract void AddForce(Vector3 position, float forceMulti);
}
