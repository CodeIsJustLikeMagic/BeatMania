using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Songtree : MonoBehaviour
{
    public GameObject interactionText;
    bool playerinside = false;

    private void Update()
    {
        if (playerinside)
        {
            if (Input.GetKeyDown("joystick button 2") || Input.GetKeyDown(KeyCode.F))
            {
                //player has picked up new song upon first interaction with tree
                //show songchange menu etc...
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            interactionText.SetActive(true);
            playerinside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            interactionText.SetActive(false);
            playerinside = false;
        }
    }
}
