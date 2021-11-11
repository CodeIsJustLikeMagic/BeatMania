using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{

    [SerializeField] protected string interactionText = "";
    [SerializeField] private bool dontShowInteractionHint = false;
    bool playerinside = false; 
    
    public void Interact()
    {
        Debug.Log("Player Interacion with "+gameObject.name,this);
        if (!dontShowInteractionHint)
        {
            InteractionHint.instance.Hide();
        }
        DoSomething();
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

            PlayerInteraction.Instance.my_interactable = gameObject;
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

            PlayerInteraction.Instance.my_interactable = null;
        }
    }
}