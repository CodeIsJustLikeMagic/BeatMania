using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmTestMetricWriter : MonoBehaviour
{
    private static RhythmTestMetricWriter _instance;

    public static RhythmTestMetricWriter Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<RhythmTestMetricWriter>();
                if (_instance == null)
                {
                    Debug.LogError("Couldn't find instance of RhythmTestMetricWriter");
                }
            }

            return _instance;
        }
    }


    public void OnCloseTest()
    {
        Debug.LogError("RhythmTestMetric Writer: Close Test not implemented", this);
    }

    public void Write(string blabla)
    {
        Debug.LogError("RhythmTestMetric Writer: Write not implemented", this);
    }
}
