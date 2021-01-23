using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongchangeSystem : MonoBehaviour
{
    public static SongchangeSystem instance;
    public AudioHelm.AudioHelmClock clock;
    public GameObject[] themes;
    public int[] bpms;
    private int maxsongs = 2;
    [SerializeField]
    private int currentsong = 1;

    private void Start()
    {
        instance = this;
        Invoke("SongChange", 0.1f);
    }

    public void SongChange()//gets called when user uses Songtree ?
    {
        SongChange(currentsong);
        //todo notify Clock that it's time to change song
        //currentsong = (currentsong + 1) % maxsongs;
    }
    public void SongChange(int song)//gets called when user uses Songtree ?
    {
        Background.instance.SetSprites(song);
        SongSynchonizeVibing.instance.RecieveSongChange(song);
        themes[currentsong].SetActive(false);
        clock.bpm = bpms[song];
        themes[song].SetActive(true);
        currentsong = song;
        //todo notify Clock that it's time to change song
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
