using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    private static PlayerInteraction _instance;

    public static PlayerInteraction Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PlayerInteraction>();
                if (_instance == null)
                {
                    Debug.LogError("Couldnt find instance of PlayerInteraction");
                }
            }
            return _instance;
        }
    }

    public GameObject my_interactable;
    public void OnInteraction(InputAction.CallbackContext value)
    {
        if (my_interactable != null && value.started)
        {
            var h = my_interactable.GetComponents<Interactable>();
            foreach (var hoi in h)
            {
                hoi.Interact();
            }
        }
    }
}
