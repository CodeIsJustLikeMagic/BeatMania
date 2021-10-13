using System;
using System.Collections;
using System.Collections.Generic;
using AudioHelm;
using UnityEngine;

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
        Debug.Log("SongChange to "+song);
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
        }else Debug.Log("That song hasn't been unlocked yet");
    }

    private void SongChange()
    {
        SongChange(currentsong);
    }

    //einfach nur zum testen
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            SongChange(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SongChange(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SongChange(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SongChange(3);
        }
    }
}
