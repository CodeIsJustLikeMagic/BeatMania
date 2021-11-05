using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VersionControll : MonoBehaviour
{
    public int VersionOverride;
    private void Awake()
    {
        if (PlayerPrefs.GetInt("Version", 0) == 0)
        {
            VersionRhythmControlled();
        }
        else
        {
            VersionNoRhythm();
        }   
    }

    void VersionRhythmControlled()
    {
        //VersionA
        MetricWriter.Instance.SetVersion(0);
        BeatChecker.Instance.RhythmControlled();
        BeatIndicator.Instance.Show();
        
    }

    void VersionNoRhythm()
    {
        //VersionB
        MetricWriter.Instance.SetVersion(1);
        BeatChecker.Instance.NoRhythm();
        BeatIndicator.Instance.Hide();
    }
}
