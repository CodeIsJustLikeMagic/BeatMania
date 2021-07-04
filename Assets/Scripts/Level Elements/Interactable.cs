using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{

    [SerializeField] protected string interactionText = "";
    
    bool playerinside = false;
    private void Update()
    {
        if (playerinside)
        {
            if (Input.GetKeyDown("joystick button 2") || Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.X))
            {
                InteractionHint.instance.Hide();
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
            InteractionHint.instance.Show(interactionText);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            playerinside = false;
            InteractionHint.instance.Hide();
        }
    }
}