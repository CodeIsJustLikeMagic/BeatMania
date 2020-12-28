﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioHelm;
using UnityEngine.Events;

public class AnimationOnBeat : MonoBehaviour
{
    public UnityEvent songChange;
    public static AnimationOnBeat instance;
    public AudioHelmClock clock;

    // Start is called before the first frame update
    public List<VibingEntity> vibingEntities = new List<VibingEntity>();
    private void Awake()
    {
        instance = this;
        clock = (AudioHelmClock)FindObjectOfType(typeof(AudioHelmClock));
    }
    public void AddMyselfToList(VibingEntity vibe)
    {
        vibingEntities.Add(vibe);
    }

    int count = 0;
    public void RecieveBeatEvent()
    {
        count++;
        //Debug.Log("recieve beat");
        if (count % 4 == 0)
        {
            //Debug.Log("accept beat" + count);
            //actual beat
            BeatChange();
        }
        //den ersten beat nicht und dann jeden 2.
    }

    public void BeatChange()
    {
        float bps = clock.bpm / 60;
        float speed = 1 / bps;
        //120 bmp / 60 = beats per second
        Debug.Log("RecieveBeat, bps " + bps + " speed " + speed);
        Debug.Log("I have " + vibingEntities.Count+" entities");
        foreach (VibingEntity e in vibingEntities)
        {
            e.OnBeat(bps);
        }   
    }
}