using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : Interactable
{
    
    // Start is called before the first frame update
    void Start()
    {
        maxparts = GameObject.FindObjectsOfType<SpaceShip>().Length;
        interactionText = "collect space ship part";
    }

    private bool is_collected = false;
    protected override void DoSomething()
    {
        Debug.Log("Interact");
        if (!is_collected)
        {
            SpaceShip.collected++;
            is_collected = true;
            gameObject.SetActive(false);
            if (collected == maxparts)
            {
                GameEnd.instance.EndReached();
            }
            SpaceShipUI.instance.PartsCollectedUpdate();
        }
    }


    public static int collected { get; set; }
    public static int maxparts { get; set; }
}
