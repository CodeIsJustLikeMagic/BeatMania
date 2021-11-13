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
    public string FileName_PlayerDamage = "Combat_Metric.csv";
    public string FileName_VariousActions = "Various_Metric.csv";
    public string VersionName = "NotSet";

    private StreamWriter BeatMetric;
    private StreamWriter CombatMetric;
    private StreamWriter Various;
    
    public void WriteBeatMetric(bool Beathit, float BeatDelta, float BeatLength,float ToleranceRange,string Action)
    {
        try
        {
            BeatMetric.WriteLine(VersionName+","+(int)Time.timeSinceLevelLoad+","+Beathit+","+BeatDelta+","+BeatLength+","+ToleranceRange+","+Action);
        }
        catch (ObjectDisposedException e)
        {
            Debug.Log("Csv file is close but we want to write to it");
            OnCloseGame();
            SetUp();
        }
    }

    public void WriteCombatMetric(string entity, float health, float hpModifier,string hitByEntity, string action)
    {
        try
        {
            CombatMetric.WriteLine(VersionName+","+(int)Time.timeSinceLevelLoad+","+entity+","+health+","+hpModifier+","+hitByEntity+","+action);
        }
        catch (ObjectDisposedException e)
        {
            Debug.Log("Csv file is close but we want to write to it");
            OnCloseGame();
            SetUp();
        }
    }

    public void WriteVariousMetric(string action)
    {
        try
        {
            Various.WriteLine(VersionName+","+(int)Time.timeSinceLevelLoad+","+action);
        }
        catch (ObjectDisposedException e)
        {
            Debug.Log("Csv file is close but we want to write to it");
            OnCloseGame();
            SetUp();
        }
    }
    
    private string getPath() 
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

    private void Awake()
    {
        SetUp();
    }

    public void SetUp()
    {
        FindOutVersion();
        var playerName = PlayerPrefs.GetString("PlayerName", "PlayerNameNotSet");
        FileName_BeatMetric= DateTimeFilePath(FileName_BeatMetric,playerName);
        FileName_PlayerDamage = DateTimeFilePath(FileName_PlayerDamage,playerName);
        FileName_VariousActions = DateTimeFilePath(FileName_VariousActions,playerName);
        Debug.Log("trying to set up beatmetrics",this);
        BeatMetric = new StreamWriter(getPath() + FileName_BeatMetric);
        CombatMetric = new StreamWriter(getPath() + FileName_PlayerDamage);
        Various = new StreamWriter(getPath() + FileName_VariousActions);
        BeatMetric.WriteLine("sep=,");
        BeatMetric.WriteLine("Version,Time,Beathit,BeatDelta,BeatLength,ToleranceRange,Action");
        CombatMetric.WriteLine("sep=,");
        CombatMetric.WriteLine("Version,Time,Entity,Health,HpModifier,HitBy,Action");
        Various.WriteLine("sep=,");
        Various.WriteLine("Version,Time,Action");
        Debug.Log("Created Metric writer at path"+getPath());
    }

    private string DateTimeFilePath(string Metric_type, string playerName)
    {
        return playerName+"_"+VersionName+"_"+DateTime.Now.ToString("dd_MM_yy_hh_mm")+"_"+Metric_type; // could add gameVersion here. Create different csv files for different game versions.
    }

    public void FindOutVersion()
    {
        SetVersion(PlayerPrefs.GetInt("Version", 0));
    }
    /// <summary>
    /// Version 0 is RhythmControlled aka Beatlocked.
    /// Version 1 is nNoRythm. The player actions dont depend on the Rhythm.
    /// </summary>
    /// <param name="versionNumber"></param>
    public void SetVersion(int versionNumber)
    {
        if (versionNumber == 0)
        {
            VersionName = "RhythmControlled";
        }
        else
        {
            VersionName = "NoRhythm";
        }   
    }

    public void OnCloseGame()
    {
        try
        {
            BeatMetric.Flush();
            BeatMetric.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        try
        {
            CombatMetric.Flush();
            CombatMetric.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        try
        {
            Various.Flush();
            Various.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

    }

    public void OnDestroy()
    {
        try
        {
            BeatMetric.Flush();
            BeatMetric.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        try
        {
            CombatMetric.Flush();
            CombatMetric.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        try
        {
            Various.Flush();
            Various.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}
