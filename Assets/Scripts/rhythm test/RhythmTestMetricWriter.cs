using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RhythmTestMetricWriter : MetricWriter
{

    public string FileName_RhythmTestMetric = "RhythmTest_Metric.csv";
    private StreamWriter BeatMetric;
    
    public override void WriteBeatMetric(bool Beathit, float BeatDelta, float BeatLength,float ToleranceRange,string Action)
    {
        try
        {
            BeatMetric.WriteLine((int)Time.timeSinceLevelLoad+","+Beathit+","+BeatDelta+","+BeatLength+","+ToleranceRange+","+Action);
        }
        catch (ObjectDisposedException e)
        {
            Debug.Log("Csv file is close but we want to write to it");
            CloseMetricWriter();
            SetUp();
        }
    }

    private void Awake()
    {
        SetUp();
    }

    public void SetUp()
    {
        var playerName = PlayerPrefs.GetString("PlayerName", "PlayerNameNotSet");
        FileName_RhythmTestMetric= DateTimeFilePath(FileName_RhythmTestMetric,playerName);
        Debug.Log("trying to set up beatmetrics",this);
        BeatMetric = new StreamWriter(getPath() + FileName_RhythmTestMetric);
        BeatMetric.WriteLine("sep=,");
        BeatMetric.WriteLine("Time,Beathit,BeatDelta,BeatLength,ToleranceRange,Action");
        Debug.Log("Created Metric writer at path"+getPath());
    }

    private string DateTimeFilePath(string Metric_type, string playerName)
    {
        return playerName+"_"+DateTime.Now.ToString("dd_MM_yy_hh_mm")+"_"+Metric_type;
    }
    
    
    public override void CloseMetricWriter()
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
    }
}
