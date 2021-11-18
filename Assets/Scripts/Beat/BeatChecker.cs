﻿using UnityEngine;
using UnityEngine.InputSystem;

public class BeatChecker : VibingEntity
{
    public static BeatChecker _instance;
    public static BeatChecker Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<BeatChecker>();
            }

            return _instance;
        }
    }

    public float toleranceShift = 0.00f;
    private float toleranceRange = 0.10f;

    public float ToleranceRange
    {
        get
        {
            return toleranceRange;
        }
        set
        {
            toleranceRange = value;
            Debug.Log("ToleranceRange set to "+value);
        }
    }

    //Hold Beat
    float beatStart = 0;
    private float beatLength = 0;

    //Callibration + UI
    private bool toggle = false;
    private int hitcount = 0;
    private int misscount = 0;
    private float missedBY = 0;
    private float hitAVG = 0;
    private bool isSet = false;

    void Update()
    {
        //Toggle UI
        if (Keyboard.current.nKey.wasPressedThisFrame)
            {
            toggle = !toggle;
            if (toggle) {
                //RESET SHIFT
                toleranceShift = 0.00f;
                hitcount = 0;
                misscount = 0;
                hitAVG = 0;
                isSet = false;
            }
        }

        //GiVE INPUT TO
        if (Keyboard.current.mKey.wasPressedThisFrame){
            if (toggle && !isSet)
            {
                //missedBY = (Time.time - beatStart);
                missedBY = (Time.time - beatStart) % beatLength;

                if (missedBY <= (ToleranceRange + toleranceShift))
                {
                    hitAVG += missedBY;
                    hitcount = hitcount + 1;
                }
                else if (missedBY >= (beatLength - (ToleranceRange - toleranceShift)))
                {
                    hitAVG -= (beatLength - missedBY);
                    hitcount = hitcount + 1;
                }
                else
                {
                    misscount = misscount + 1;
                }
                if (hitcount == 10) {
                    //sollte nur einmal setzen
                    toleranceShift += (hitAVG / 10);
                    isSet = true;
                }
            }
        }
    }

    /// <summary>
    /// Checks if current time is in musik beat.
    /// </summary>
    /// <returns>true if Time.time is in beat</returns>
    public bool IsInBeat()//is current time in beat?
    {
        return IsInBeat(Time.time, ToleranceRange, toleranceShift);
    }

    public bool IsInBeat(string Metric_Action)
    {
        float missedBySeconds = (Time.time - beatStart) % beatLength;
        bool b = missedBySeconds <= (ToleranceRange + toleranceShift) || missedBySeconds >= beatLength - (ToleranceRange - toleranceShift);
        MetricWriter.Instance.WriteBeatMetric(b,missedBySeconds,beatLength,ToleranceRange, Metric_Action);
        if (allwaysTrue)
        {
            return true;
        }
        return b;
    }
    
    public bool IsInBeatRhythmTest(string Metric_Action)
    {
        float missedBySeconds = (Time.time - beatStart) % beatLength;
        bool b = missedBySeconds <= (ToleranceRange + toleranceShift) || missedBySeconds >= beatLength - (ToleranceRange - toleranceShift);
        RhythmTestMetricWriter.Instance.WriteBeatMetric(b,missedBySeconds,beatLength,ToleranceRange, Metric_Action);
        if (allwaysTrue)
        {
            return true;
        }
        return b;
    }

    /// <summary>
    /// Checks if given time is in musik beat.
    /// </summary>
    /// <param name="time"></param>
    /// <param name="toleranceRange"></param>
    /// <returns>true if time is in beat</returns>
    public bool IsInBeat(float time, float toleranceRange, float toleranceShift)
    {
        if (allwaysTrue)
        {
            return true;
        }
        // time is in beat if: time == beatStart + n * beatLegnth + -toleranceRange; where n is any natural number
        float missedBySeconds = (time - beatStart) % beatLength;
        return missedBySeconds <= (toleranceRange + toleranceShift) || missedBySeconds >= beatLength - (toleranceRange - toleranceShift);
        //return missedBySeconds <= toleranceRange || missedBySeconds >= beatLength - toleranceRange; old
    }

    public float IsInBeatDelta()
    {
        return (Time.time - beatStart) % beatLength;
    }

    public float BeatLength()
    {
        return beatLength;
    }


    public void OnGUI()
    {
        if (toggle)
        {
            GUI.contentColor = Color.black;
            GUI.Label(new Rect(10, 25, 200, 500), "HIT_COUNT: " + hitcount);
            GUI.Label(new Rect(10, 50, 200, 500), "MISS_COUNT: " + misscount);
            GUI.Label(new Rect(10, 75, 200, 500), "CURRENT_SHIFT: " + toleranceShift);
            GUI.Label(new Rect(10, 100, 200, 500), "CURRENT_RANGE: " + ToleranceRange);
            GUI.Label(new Rect(10, 125, 200, 500), "BEAT: " + beatStart);
            GUI.Label(new Rect(10, 150, 200, 500), "TIME: " + Time.time);
            GUI.Label(new Rect(10, 175, 200, 500), "DIFF: " + (Time.time - beatStart));
            GUI.Label(new Rect(10, 200, 200, 500), "MISSED_BY: " + missedBY);
            GUI.Label(new Rect(10, 225, 200, 500), "HIT_AVG: " + hitAVG);
            GUI.Label(new Rect(10, 250, 200, 500), "SHIFT_POS: " + (ToleranceRange + toleranceShift));
            GUI.Label(new Rect(10, 275, 200, 500), "SHIFT_NEG: " + (ToleranceRange - toleranceShift));
            GUI.Label(new Rect(10, 300, 200, 500), "ADJUSTED: " + isSet);
        }
    }

    /// <summary>
    /// Checks if given time is in musik beat.
    /// </summary>
    /// <param name="time"></param>
    /// <returns>true if time is in beat</returns>
    public bool IsInBeat(float time)
    {
        return IsInBeat(time, ToleranceRange, ToleranceRange);
    }

    public float IsInBeatMissedBy(float time)
    {
        return (time - beatStart) % beatLength;
    }

    public override void OnBeat(float jitter_delay, float bps)
    {
        beatLength = 1 / bps;
    }

    public void SetBeatStart(float beatStart)
    {
        this.beatStart = beatStart;
    }

    public float GetbeatLength() {
        return beatLength;
    }

    private bool allwaysTrue = false;
    public void DisableBeatCheck()
    {
        allwaysTrue = true;
    }

    public void RhythmControlled()
    {
        allwaysTrue = false;
    }

    public void NoRhythm()
    {
        allwaysTrue = true;
    }
}
