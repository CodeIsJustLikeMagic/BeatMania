using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;


public class PlaytestInstructions : MonoBehaviour
{
    public bool skipInstructions = false;
    public GameObject stuff;
    public static PlaytestInstructions instance;
    private int sucess_count = 0;
    private int failed_count = 0;

    public Text hit_text;
    public Text missed_text;
    public Text missed_by;

    public Text result_text;
    public Text saved_to;

    [SerializeField] private AudioSource click;
    [SerializeField] private AudioSource acknowledge;

    public List<GameObject> instruction_displays;
    public void Success()
    {
        sucess_count++;
        missed_by.text = "missed by: " + BeatChecker.instance.IsInBeatMissedBy(Time.time) +" range(0, "+SongSynchonizeVibing.instance.BeatLength+")";
        data += "\nS" + (SongchangeSystem.instance.Currentsong + 1) + " hit. missed by !" +
                BeatChecker.instance.IsInBeatMissedBy(Time.time)+"! beat length "+SongSynchonizeVibing.instance.BeatLength;
        UpdateCounters();
    }

    public void Failed()
    {
        failed_count++;
        missed_by.text = "missed by: " + BeatChecker.instance.IsInBeatMissedBy(Time.time) +" range(0, "+SongSynchonizeVibing.instance.BeatLength+")";
        data += "\nS" + (SongchangeSystem.instance.Currentsong + 1) + " miss. missed by !" +
                BeatChecker.instance.IsInBeatMissedBy(Time.time)+"! beat length "+SongSynchonizeVibing.instance.BeatLength;
        UpdateCounters();
    }

    private void Awake()
    {
        instance = this;
        ShowInstruction(0);
        if (skipInstructions)
        {
            currentInstruction = instruction_displays.Count;
            HideAll();
        }
    }

    // Update is called once per frame

    void AdvanceWithEnter(int changeToSong)
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            currentInstruction++;
            ShowInstruction(currentInstruction);
            ResetCounters();
            ClickSound();
            SongchangeSystem.instance.SongChange(changeToSong);
        }
    }

    void ShowOnlyHUD()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            HideAll();
            //stuff.SetActive(true);
        }
    }

    public string data;
    void Save(string context)
    {
        if (context == "") return;
        data = data + "\n" + context + " h: " + sucess_count + " m: " + failed_count;
        //result_text.text = data;
        var fileName = Application.dataPath + "/BeatMania_Playtest_1_Result.txt";
        saved_to.text = fileName;
        var sr = File.CreateText(fileName);
        sr.Write(data);
        sr.Close();
    }

    void AdvanceWithKHits(int k, string save_message)
    {
        if (sucess_count == k || failed_count == k)
        {
            Save(save_message);
            currentInstruction++;
            ShowInstruction(currentInstruction);
            ResetCounters();
            AcknowledgeSound();
        }

        if (sucess_count == 1 || failed_count == 1)
        {
            ShowOnlyHUD();
        }
    }
    

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ResetCounters();
        }

        switch (currentInstruction)
        {
            case 0: // Instrcution Hello
                stuff.SetActive(false);
                AdvanceWithEnter(0);
                break;
            case 1: // s1 Eyes open
                stuff.SetActive(true);
                AdvanceWithKHits(20, "S1 eyes open");
                break;
            case 2: 
                AdvanceWithKHits(20,"S1 eyes closed");
                break;
            case 3:
                AdvanceWithEnter(1);
                break;
            case 4:
                AdvanceWithKHits(20,"S2 eyes open");
                break;
            case 5:
                AdvanceWithKHits(20,"S2 eyes closed");
                break;
            case 6:
                AdvanceWithEnter(2);
                break;
            case 7:
                AdvanceWithKHits(20,"S3 eyes open");
                break;
            case 8:
                AdvanceWithKHits(20,"S3 eyes closed");
                break;
            case 9:
                AdvanceWithEnter(3);
                break;
            case 10:
                AdvanceWithKHits(20,"S4 eyes open");
                break;
            case 11:
                AdvanceWithKHits(20,"S4 eyes closed");
                break;
            case 12:
                AdvanceWithEnter(0);
                break;
            case 13:
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    // let the player explore what we have.
                    HideAll();
                    stuff.SetActive(false);
                }
                break;
        }
    }

    private void ClickSound()
    {
        click.Play();
    }

    private void AcknowledgeSound()
    {
        acknowledge.Play();
    }
    
    void UpdateCounters()
    {
        hit_text.text = "hit: " + sucess_count;
        missed_text.text = "missed: " + failed_count;
    }
    private int currentInstruction = 0;

    void ResetCounters()
    {
        failed_count = 0;
        sucess_count = 0;
        UpdateCounters();
    }
    void HideAll()
    {
        foreach(var display in instruction_displays)
        {
            display.SetActive(false);
        }
    }
    void ShowInstruction(int instructionIndex)
    {
        HideAll();
        instruction_displays[instructionIndex].SetActive(true);
    }
}
