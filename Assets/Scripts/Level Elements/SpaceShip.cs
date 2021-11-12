using System;
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

    public bool is_collected = false;
    [Space(10)] public string entity = "spaceship";

    public void Collect()
    {
        DoSomething();
    }
    protected override void DoSomething()
    {
        if (!is_collected)
        {
            SpaceShip.collected++;
            is_collected = true;
            gameObject.SetActive(false);
            if (collected >= maxparts)
            {
                GameEnd.Instance.EndReached();
                MetricWriter.Instance.WriteVariousMetric("Game End");
            }
            SpaceShipUI.instance.PartsCollectedUpdate();
            MetricWriter.Instance.WriteVariousMetric("SpaceShip collected "+entity);
        }
        
    }

    private void OnDestroy()
    {
        SpaceShip.collected = 0;
    }


    public static int collected { get; set; }
    public static int maxparts { get; set; }
}
