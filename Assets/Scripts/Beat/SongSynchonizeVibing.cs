using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;
using AudioHelm;
using UnityEditor.Rendering;
using UnityEngine.Events;


//manages timing and speed synchonisation for vibingEntiries with the clock
public class SongSynchonizeVibing : MonoBehaviour
{
    public static SongSynchonizeVibing instance;
    public AudioHelmClock clock;
    private int currentsong = 0;

    public Sequencer BeatGiver;

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

    private float delay;
    private int count = 0;
    public int everyXbeats = 4;
    private float beat_length_seconds;
    public bool[] delay_beat_timing;

    public float beatStart = 0;
    public void RecieveBeatEvent(int division)//an den sequencer drangehängt
    {
        if (division % everyXbeats == 0) // very 4th division
        {
            //Debug.Log("Beat at Division "+ division +" time is "+ t + "counts as inbeat: "+ BeatChecker.instance.IsInBeat(t + (1/(60/clock.bpm))/16f));
            //Debug.Log(" missed By !!!"+ BeatChecker.instance.IsInBeatMissedBy(t + delay));
            //Debug.Log("Delay "+delay);
            //Debug.Log("sixteenth time "+ BeatGiver.GetSixteenthTime()+" Sequencer Position "+ BeatGiver.GetSequencerPosition()); 
            if (songHasBeenChanged)
            {

                delay = BeatGiver.GetSixteenthTime() / 2; // half a division
                delay = delay * 1.1f; // make beat timing at a little more than half a note 
                
                var sixteenth_time = BeatGiver.GetSixteenthTime();
                beat_length_seconds = 60 / clock.bpm;
                
                float sequencer_position_jitter = (float)BeatGiver.GetSequencerPosition()%1;
                Debug.Log(Time.time+" Sequencer Position Jitter "+ sequencer_position_jitter);
                
                var time_that_event_is_late = sequencer_position_jitter * sixteenth_time; // jitter * beatlength_time
                // actual division is at Time.time - time_that_event_is_late
                //division is before the actual sound. add a small delay here so the beat counts as a little after middle of the actual beat sound
                beatStart = Time.time - time_that_event_is_late + delay;

                var wait_time = (1 - sequencer_position_jitter) * BeatGiver.GetSixteenthTime();
                BeatChecker.instance.SetBeatStart(beatStart);
                NotifyVibingEntities((sequencer_position_jitter / 4) - (0.125f / 1)); 
                // jitter_offset is the position_jitter in relation to the entire beat (4 divisions)
                // and adding a delay so the start of the vibing plants animations is at the beat_sound
                songHasBeenChanged = false;
                Debug.Log(Time.time+" corrected beattiming " + beatStart +" next beat should start at "+ (beatStart + beat_length_seconds));
            }
            else
            {
                //check if audio helm beat timing still fits beatchecker. If not reset beat timing because it got messed up.
                var jitter = (float) BeatGiver.GetSequencerPosition() % 1;
                var beattiming = Time.time - (jitter * BeatGiver.GetSixteenthTime()) + delay;
                Debug.Log(Time.time+" jitter "+ jitter+" calculated beattiming is "+ beattiming+" beat is still synched: "+ BeatChecker.instance.IsInBeat(beattiming,0.07f,0.0f));
                Debug.Log("missed Beat by "+ BeatChecker.instance.IsInBeatMissedBy(beattiming));
                if (!BeatChecker.instance.IsInBeat(beattiming, 0.07f, 0.0f))
                {
                    Debug.LogError("Fixing sync");
                    songHasBeenChanged = true;
                }
            }
        }
    }

    public void NotifyVibingEntities(float jitter_offset)
    {
        float bps = clock.bpm / 60;
        foreach (VibingEntity e in vibingEntities)
        {
            if(e != null)
            {
                e.OnBeat(jitter_offset, bps);
            }
        }
    }

    private bool songHasBeenChanged = false;
    public void RecieveSongChange(int song)
    {
        currentsong = song;
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
