using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseHealthBehavior : MonoBehaviour
{
    abstract public void ApplyDamage(float dmg, bool stagger, Vector3 attack_pos);
    
}
