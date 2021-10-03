using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFeedback : MonoBehaviour
{
    public GameObject normalModel;
    public GameObject damageModel;

    public void displayDamage()
    {
        
        damageModel.SetActive(true);
        normalModel.SetActive(false);

    }

    public void doNotDisplayDamage()
    {
        normalModel.SetActive(true);
        damageModel.SetActive(false);
    }
}
