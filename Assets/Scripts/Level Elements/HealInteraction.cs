using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealInteraction : Interactable
{
    protected override void DoSomething()
    {
        FindObjectOfType<CharacterController>().ApplyHeal(10000,gameObject.name);
    }
}
