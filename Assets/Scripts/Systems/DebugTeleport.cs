using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DebugTeleport : MonoBehaviour
{
    // Start is called before the first frame update
    private int current = 0;
    private Checkpoint[] spots;
    void Start()
    {
        spots = GameObject.FindObjectsOfType<Checkpoint>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)) // lock behind debug mode?
        {
            CharacterController.instance.DebugTeleport(spots[current].getPosition());
            current++;
        }
    }
}
