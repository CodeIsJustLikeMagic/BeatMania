using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    bool playerinside = false;
    [SerializeField] protected ProximityText proximityText;
    

    private void Start()
    {
        proximityText = GetComponentInChildren<ProximityText>();
    }

    private void Update()
    {
        if (playerinside)
        {
            if (Input.GetKeyDown("joystick button 2") || Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.X))
            {
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
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            playerinside = false;
        }
    }
}