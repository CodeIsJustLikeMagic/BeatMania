using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseHealthBehavior : MonoBehaviour
{
    public float Health = 10f;

    private void OnTriggerEnter(Collider collider)
    {
        HandleCollision(collider);
    }

    abstract protected void HandleCollision(Collider hit);
    
}
