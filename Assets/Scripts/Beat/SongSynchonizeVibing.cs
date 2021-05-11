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

    public void RemoveFromList(VibingEntity vibe)
    {
        vibingEntities.Remove(vibe);
    }

    private int count = 0;
    public int everyXbeats = 4;
    public void RecieveBeatEvent(int division)//an den sequencer drangehängt
    {
        count++;
        //Debug.Log("count "+count +" time "+Time.time +" is division "+ division);
        if (true)//songHasBeenChanged)
        {
            if (division % everyXbeats == 0) // very 4th division
            {
                if (songHasBeenChanged)
                {
                    songHasBeenChanged = false;
                    Invoke("NotifyVibingEntities", (1/(60/clock.bpm))/16f);// beat event is at audio helm synthesizer division
                    //division is before the actual sound. add a small delay here so the beat counts as the middle of the actual beat sound
                    //NotifyVibingEntities();
                }
            }
        }
    }

    public void NotifyVibingEntities()
    {
        float bps = clock.bpm / 60;
        //120 bmp / 60 = beats per second
        foreach (VibingEntity e in vibingEntities)
        {
            if(e != null)
            {
                e.OnBeat(bps);
            }
        }
    }

    private bool songHasBeenChanged = false;
    public void RecieveSongChange(int song)
    {
        songHasBeenChanged = true;
        foreach (VibingEntity e in vibingEntities)
        {
            if(e != null)
            {
                e.OnSongChange(song);
            }
        }
    }

}
