using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class rhythmTest : MonoBehaviour
{
    public void OnRhythmTestInput(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            Debug.Log("Player Input rhythm test");
            RhythmTestMetricWriter.Instance.Write("Blabla");
            //write into metric. new metric file for this. Custom metric writer script probably
            //count how many the player did. After like 20 it should be enough.
            //make the indicator invisible after a while
            //still need a thing that shows what button to press
        }
    }
}
