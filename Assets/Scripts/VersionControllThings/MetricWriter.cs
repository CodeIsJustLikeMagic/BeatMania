using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;


public abstract class MetricWriter : MonoBehaviour
{
    private static MetricWriter _instance;

    public static MetricWriter Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<MetricWriter>();
                if (_instance == null)
                {
                    Debug.LogError("Couldn't find instance of Metric_Writer");
                }
            }

            return _instance;
        }
    }


    public abstract void WriteBeatMetric(bool Beathit, float BeatDelta, float BeatLength, float ToleranceRange,
        string Action);

    public virtual void WriteCombatMetric(string entity, float health, float hpModifier, string hitByEntity, string action){}

    public virtual void WriteVariousMetric(string action){}

    public static string getPath() 
    {
        #if UNITY_EDITOR
        var filePath =  Application.dataPath + "/../GameStats/";
        #else
        var filePath =  Application.dataPath+"/../GameStats/";
        #endif

        try
        {
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
        }catch(IOException ex)
        {
            Debug.LogError(ex.Message);
        }

        return filePath;
    }

    public abstract void CloseMetricWriter();

    public virtual void SetVersion(int i)
    {
    }
}
