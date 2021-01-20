using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioHelm;
using UnityEngine.Events;


//manages timing and speed synchonisation for vibingEntiries with the clock
public class SongSynchonizeVibing : MonoBehaviour
{
    public static SongSynchonizeVibing instance;
    public AudioHelmClock clock;

    // Start is called before the first frame update
    public List<VibingEntity> vibingEntities = new List<VibingEntity>();
    private void Awake()
    {
        instance = this;
        clock = (AudioHelmClock)FindObjectOfType(typeof(AudioHelmClock));
    }
    public void AddMyselfToList(VibingEntity vibe)//VibingEntities add themselves to my list. Like a bootleg Observer
    {
        vibingEntities.Add(vibe);
    }

    public int count = 0;
    public int everyXbeats = 4;
    public void RecieveBeatEvent()
    {
        //Debug.Log("recieve beat");
        if (count % everyXbeats == 0 && count > 0)
        {
            //Debug.Log("accept beat" + count);
            //actual beat
            NotifyVibingEntities();
        }
        count++;
        //den ersten beat nicht und dann jeden 2.
    }

    public void NotifyVibingEntities()
    {
        float bps = clock.bpm / 60;
        //120 bmp / 60 = beats per second
        Debug.Log("RecieveBeat, bps " + bps);
        Debug.Log("I have " + vibingEntities.Count + " entities");
        foreach (VibingEntity e in vibingEntities)
        {
            e.OnBeat(bps);
        }
    }
    
    public void RecieveSongChange(int song)
    {
        foreach (VibingEntity e in vibingEntities)
        {
            e.OnSongChange(song);
        }
    }

}
