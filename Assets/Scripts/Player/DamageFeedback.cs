using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFeedback : MonoBehaviour
{
    public GameObject normalModel;
    public GameObject damageModel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

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
