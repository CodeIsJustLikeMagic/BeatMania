using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damageFeedback : MonoBehaviour
{

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

    }

    public void doNotDisplayDamage()
    {
        damageModel.SetActive(false);
    }
}
