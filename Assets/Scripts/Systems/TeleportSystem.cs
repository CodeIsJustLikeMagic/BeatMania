using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportSystem : MonoBehaviour
{
    private static TeleportSystem _instance;
    
    public static TeleportSystem Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<TeleportSystem>();
            }

            return _instance;
        }
    }
    
    public Checkpoint[] checkpoints;

    public void ActivateCheckpoint(int point)
    {
        Debug.Log("Activating checkpoint "+point);
        if (point == -1)
        {
            return;
        }
        checkpoints[point].Activate();
        Debug.Log("Activating checkpoint "+point, checkpoints[point]);
    }

    public int GetActiveCheckpointIndex()
    {
        for (int i = 0; i < checkpoints.Length; i++)
        {
            if (checkpoints[i].active)
            {
                return i;
            }
        }

        return -1;
    }
}
