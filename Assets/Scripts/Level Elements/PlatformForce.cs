using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlatformForce : MonoBehaviour
{

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            // make player our child
            other.collider.transform.SetParent(transform);
            // if the players transform is to low, which would mean not quite standing on the platform, glitch them on top
            if (-1f < other.gameObject.transform.position.y && other.gameObject.transform.position.y < 0.5f)
            {
                CharacterController.instance.SetyPosition(0.4918084f);
                //other.gameObject.transform.position.y = 0.5f;
            }
        }
    }

    public void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.collider.transform.SetParent(null);
        }
    }
}
