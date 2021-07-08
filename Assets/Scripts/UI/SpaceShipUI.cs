using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpaceShipUI : MonoBehaviour
{
    public static SpaceShipUI instance;
    [SerializeField] private Text t = null;
    

    public void PartsCollectedUpdate()
    {
        t.text = SpaceShip.collected + "/" + SpaceShip.maxparts;
    }
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        Invoke("PartsCollectedUpdate",1);
    }
}
