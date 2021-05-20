using System.Collections;
using System.Collections.Generic;
using AudioHelm;
using UnityEngine;

public class SongchangeSystem : MonoBehaviour
{
    public static SongchangeSystem instance;
    public AudioHelm.AudioHelmClock clock;
    public GameObject[] themes;
    public int[] bpms;
    [Tooltip("will Initialize with this song at start")]
    [SerializeField]
    private int currentsong = 0;

    public int Currentsong
    {
        get => currentsong;
    }
    [SerializeField]
    private bool enableAllSongs = false;

    private void Start()
    {
        instance = this;
        Invoke("SongChange", 0.2f);
    }

    public int GetCurrentSong()
    {
        return currentsong;
    }

    public void SongChange(int song)//gets called when user uses Songtree ?
    {
        if (UnlockedSongs.instance.SongIsUnlocked(song) || enableAllSongs)
        {
            SongSynchonizeVibing.instance.RecieveSongChange(song);
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
