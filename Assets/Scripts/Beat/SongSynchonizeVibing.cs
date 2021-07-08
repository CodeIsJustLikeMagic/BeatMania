using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;
using AudioHelm;
using UnityEngine.Events;


//manages timing and speed synchonisation for vibingEntiries with the clock
public class SongSynchonizeVibing : MonoBehaviour
{
    public static SongSynchonizeVibing instance;
    public AudioHelmClock clock;
    public Sequencer BeatGiver;
    
    private float[] delay_modifier_per_song = {1.7f, 1.2f, 1.6f, 1.3f};
    // a higer modifier means the beat counts later in the division
    // if you hit mostly to early the modifier needs to be lower
    // if you hit to late the modifier needs to be higher
    
    // you hit early if BeatChecker missed_by gives you close to beat length
    // you hit late if BeatChecker gives you close to 0
    
    // a modifier of 1.0 means that the beat counts as the middle each 4th division
    // a modifier of 0 would be exaclty the 4th division
    // a modifier of 2 would be 4th + 1 division.
    
    private int currentsong = 0;
    
    // lits gets filled automatically
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
    public int everyXbeats = 4;

    public bool[] delay_beat_timing;

    private float beatStart = 0;
    public float BeatStart
    {
        get => beatStart;
    }

    private float beat_length_seconds;
    public float BeatLength
    {
        get => beat_length_seconds;
    }
    
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
                delay = delay * delay_modifier_per_song[currentsong]; // * 1.7f // make beat timing at a little more than half a note 
                
                var sixteenth_time = BeatGiver.GetSixteenthTime();
                beat_length_seconds = 60 / clock.bpm;
                
                float sequencer_position_jitter = (float)BeatGiver.GetSequencerPosition()%1;
                
                var time_that_event_is_late = sequencer_position_jitter * sixteenth_time; // jitter * beatlength_time
                // actual division is at Time.time - time_that_event_is_late
                //division is before the actual sound. add a small delay here so the beat counts as a little after middle of the actual beat sound
                beatStart = Time.time - time_that_event_is_late + delay;

                //var wait_time = (1 - sequencer_position_jitter) * BeatGiver.GetSixteenthTime();
                BeatChecker.instance.SetBeatStart(beatStart);
                NotifyVibingEntities( -(0.125f* delay_modifier_per_song[currentsong]) + (sequencer_position_jitter / 4)); 
                // jitter_offset is the position_jitter in relation to the entire beat (4 divisions)
                // and adding a delay so the start of the vibing plants animations is at the beat_sound
                songHasBeenChanged = false;
                //Debug.Log(Time.time+" corrected beattiming " + beatStart +" next beat should start at "+ (beatStart + beat_length_seconds));
            }
            else
            {
                //check if audio helm beat timing still fits beatchecker. If not reset beat timing because it got messed up.
                var jitter = (float) BeatGiver.GetSequencerPosition() % 1;
                var beattiming = Time.time - (jitter * BeatGiver.GetSixteenthTime()) + delay;
                //Debug.Log(Time.time+" jitter "+ jitter+" calculated beattiming is "+ beattiming+" beat is still synched: "+ BeatChecker.instance.IsInBeat(beattiming,0.07f,0.0f));
                //Debug.Log("missed Beat by "+ BeatChecker.instance.IsInBeatMissedBy(beattiming));
                try
                {
                    if (!BeatChecker.instance.IsInBeat(beattiming, 0.07f, 0.0f))
                    {
                        Debug.Log("Fixing sync");
                        songHasBeenChanged = true;
                    }
                }
                catch{}

            }
        }
    }

    public float GetJitterOffset()
    {
        float sequencer_position_jitter = (float)BeatGiver.GetSequencerPosition()%1;
        return -(0.125f * delay_modifier_per_song[currentsong]) + (sequencer_position_jitter / 4);
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
