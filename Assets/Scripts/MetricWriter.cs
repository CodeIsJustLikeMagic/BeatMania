using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;


public class MetricWriter : MonoBehaviour
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
    
    public string FileName_BeatMetric = "Beat_Metric.csv";
    public string FileName_PlayerDamage = "PlayerDamage_Metric.csv";
    public string FileName_VariousActions = "Various_Metric.csv";

    private StreamWriter BeatMetric;
    private StreamWriter PlayerDamage;
    private StreamWriter Various;
    
    public void WriteBeatMetric(bool Beathit, float BeatDelta, float BeatLength,string Action)
    {
        BeatMetric.WriteLine(Time.time+","+Beathit+","+BeatDelta+","+BeatLength+","+Action);
    }

    public void WritePlayerDamageMetric(float health, string hitBy, string damaged)
    {
        PlayerDamage.WriteLine(Time.time+","+health+","+hitBy);
        
    }

    public void WriteVariousMetric(string action)
    {
        Various.WriteLine(Time.time+","+action);
    }
    
    private string getPath() 
    {
        #if UNITY_EDITOR
        var filePath =  Application.dataPath + "/GameStats/";
        #else
        var filePath =  Application.dataPath+"/";
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

    private void Awake()
    {
        FileName_BeatMetric= DateTimeFilePath(FileName_BeatMetric);
        FileName_PlayerDamage = DateTimeFilePath(FileName_PlayerDamage);
        FileName_VariousActions = DateTimeFilePath(FileName_VariousActions);
        
        BeatMetric = new StreamWriter(getPath() + FileName_BeatMetric);
        PlayerDamage = new StreamWriter(getPath() + FileName_PlayerDamage);
        Various = new StreamWriter(getPath() + FileName_VariousActions);
        BeatMetric.WriteLine("Time,Beathit,BeatDelta,Action");
        PlayerDamage.WriteLine("Time,Health,HitBy");
        Various.WriteLine("Time,Action");
        Debug.Log("Created Metric writer at path"+getPath());
    }

    private string DateTimeFilePath(string original_name)
    {
        return DateTime.Now.ToString("dd_mm_yy_hh_mm")+"_"+original_name; // could add gameVersion here. Create different csv files for different game versions.
    }
    public void OnDestroy()
    {
        BeatMetric.Flush();
        BeatMetric.Close();
        
        PlayerDamage.Flush();
        PlayerDamage.Close();
        
        Various.Flush();
        Various.Close();
    }



}
