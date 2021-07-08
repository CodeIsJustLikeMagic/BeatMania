using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaytestManager : MonoBehaviour
{
    [SerializeField] private BeatChecker beatChecker  = null;
    [SerializeField] private GameObject beatIndicator = null;
    [SerializeField] private bool playerControllWithBeat = true;

    private void Start()
    {
        if (!playerControllWithBeat)
        {
            beatChecker.DisableBeatCheck();
            beatIndicator.SetActive(false);
        }
    }
}
