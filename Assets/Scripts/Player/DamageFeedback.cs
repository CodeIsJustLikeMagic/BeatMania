using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFeedback : MonoBehaviour
{
    public GameObject normalModel;
    public GameObject damageModel;
    public GameObject shieldModel;

    public void displayDamage()
    {
        damageModel.SetActive(true);
        normalModel.SetActive(false);
        shieldModel.SetActive(false);
        Debug.Log("DamageFeedback red. InBeat? "+BeatChecker.Instance.IsInBeat(), this);
    }

    public void doNotDisplayDamage()
    {
        normalModel.SetActive(true);
        damageModel.SetActive(false);
        shieldModel.SetActive(false);
    }

    public void displayShieldModel()
    {
        normalModel.SetActive(false);
        damageModel.SetActive(false);
        shieldModel.SetActive(true);
    }
}
