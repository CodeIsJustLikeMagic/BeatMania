using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlatformForce : MonoBehaviour
{

    private bool _playerIsStandingOnMe = false;
    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            // make player our child
            other.collider.transform.SetParent(transform);
            _playerIsStandingOnMe = true;
            // if the players transform is to low, which would mean not quite standing on the platform, glitch them on top
            if (-1f < other.gameObject.transform.position.y && other.gameObject.transform.position.y < 0.5f)
            {
                CharacterController.Instance.SetyPosition(0.4918084f);
                //other.gameObject.transform.position.y = 0.5f;
            }
        }
    }

    private void OnDisable()
    {
        if (_playerIsStandingOnMe)
        {
            CharacterController.Instance.gameObject.transform.SetParent(null);
            _playerIsStandingOnMe = false;
        }
    }

    public void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.collider.transform.SetParent(null);
            _playerIsStandingOnMe = false;
        }
    }

    public void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (-1f < other.gameObject.transform.position.y && other.gameObject.transform.position.y < 0.5f)
            {
                CharacterController.Instance.SetyPosition(0.4918084f);
            }
        }
    }
}
