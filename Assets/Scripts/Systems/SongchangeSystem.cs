using System;
using System.Collections;
using System.Collections.Generic;
using AudioHelm;
using UnityEngine;
using UnityEngine.InputSystem;

public class SongchangeSystem : MonoBehaviour
{
    private static SongchangeSystem _instance;
    public static SongchangeSystem Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SongchangeSystem>();
            }

            return _instance;
        }
    }
    
    public AudioHelm.AudioHelmClock clock;
    public GameObject[] themes;
    public int[] bpms;
    [Tooltip("will Initialize with this song at start")]
    [SerializeField]
    private int currentsong = 0;

    public int Currentsong
    {
        get => currentsong;
        set => currentsong = value;
    }

    [SerializeField]
    private bool enableAllSongs = false;

    private void Start()
    {
        Invoke("SongChange", 0.2f);
    }

    public int GetCurrentSong()
    {
        return currentsong;
    }

    public void SongChange(int song)//gets called when user uses Songtree ?
    {
        //Debug.Log("SongChange to "+song);
        if (UnlockedSongs.Instance.SongIsUnlocked(song) || enableAllSongs)
        {
            foreach (var theme in themes)
            {
                theme.SetActive(false);
            }
            SongSynchonizeVibing.Instance.RecieveSongChange(song);
            themes[currentsong].SetActive(false);
            clock.bpm = bpms[song];
            themes[song].SetActive(true);
            currentsong = song;
            MetricWriter.Instance.WriteVariousMetric("SongChange "+song);
        }else Debug.Log("That song hasn't been unlocked yet");
    }

    private void SongChange()
    {
        SongChange(currentsong);
    }

    //einfach nur zum testen
    public void Update()
    {
        #if UNITY_EDITOR
        if (Keyboard.current.numpad0Key.wasPressedThisFrame) 
        {
            SongChange(0);
        }
        if (Keyboard.current.numpad1Key.wasPressedThisFrame)
        {
            SongChange(1);
        }
        if (Keyboard.current.numpad2Key.wasPressedThisFrame)
        {
            SongChange(2);
        }
        if (Keyboard.current.numpad3Key.wasPressedThisFrame)
        {
            SongChange(3);
        }
        #endif
    }
}
