using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{

    [SerializeField] protected string interactionText = "";
    [SerializeField] private bool dontShowInteractionHint = false;
    bool playerinside = false;
    private void Update()
    
    {
        if (playerinside)
        {
            if (Input.GetKeyDown("joystick button 2") || Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.X))
            {
                if (!dontShowInteractionHint)
                {
                    InteractionHint.instance.Hide();
                }
                DoSomething();
            }
        }
    }

    protected abstract void DoSomething();
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            playerinside = true;
            if (!dontShowInteractionHint)
            {
                InteractionHint.instance.Show(interactionText);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            playerinside = false;
            if (!dontShowInteractionHint)
            {
                InteractionHint.instance.Hide();
            }
        }
    }
}