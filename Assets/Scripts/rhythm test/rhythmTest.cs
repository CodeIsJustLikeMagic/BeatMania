using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class rhythmTest : MonoBehaviour
{
    public int maxtap = 20;
    private int tapcount = 0;

    public UnityEvent TapEvent;
    public UnityEvent TestDoneEvent;
    public UnityEvent SecondTestDone;

    private void Awake()
    {
        if (TapEvent == null)
        {
            TapEvent = new UnityEvent();
        }

        if (TestDoneEvent == null)
        {
            TestDoneEvent = new UnityEvent();
        }
        
        if (SecondTestDone == null)
        {
            SecondTestDone = new UnityEvent();
        }
    }


    public void OnRhythmTestInput(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            //Debug.Log("Player Input rhythm test");
            tapcount++;
            TapEvent.Invoke();

            if (tapcount <= maxtap)
            {
                BeatChecker.Instance.IsInBeatRhythmTest("with_indicator");
            }
            else
            {
                BeatChecker.Instance.IsInBeatRhythmTest("no_indicator");
            }
            if (tapcount == maxtap)
            {
                TestDoneEvent.Invoke();
            }else if (tapcount == 2 * maxtap)
            {
                SecondTestDone.Invoke();
            }
            //count how many the player did. After like 20 it should be enough.
            //make the indicator invisible after a while
            //still need a thing that shows what button to press
        }
    }
}
