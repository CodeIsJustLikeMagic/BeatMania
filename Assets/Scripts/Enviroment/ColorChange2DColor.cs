using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;
using Debug = UnityEngine.Debug;

[DisallowMultipleComponent]
public class ColorChange2DColor : ColorChange
{
    [SerializeField]
    private SpriteRenderer sprite;
    [SerializeField]
    private Color[] colors = { new Color(255, 255, 255, 255), new Color(255, 255, 255, 255), new Color(255, 255, 255, 255) };
    [SerializeField] private float SongChangeColorSteps = 5;

    [SerializeField] private float delayTransitionByXBeats = 0;
    private int currentSong;
    private bool songWasChanged = false;
    private float beatCountsToTransition = 1;

    public new void Start()
    {
        if (debug)
        {
            Debug.Log("platform running Start");
        }
        base.Start();
    }
    
    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        if(sprite == null)
        {
            sprite = GetComponentInChildren<SpriteRenderer>();
            if(sprite == null)
            {
                Debug.LogError(gameObject.name + " Element is missing its spriteRenderer");
            }
        }
    }

    public bool debug = false;

    // show color gets called when the song is changed.
    protected override void showColor(int song)
    {
        if (debug)
        {
            Debug.Log("show Color was called. Song "+song);
        }
        if (colors.Length == 0)
        {
            Debug.LogError("color Array Length is zero", this);
        }
        if (!Application.isPlaying)
        {
            sprite = GetComponentInChildren<SpriteRenderer>();
            sprite.color = colors[song % colors.Length];
        }
        else
        {
            //Debug.Log("show Color");
            currentSong = song;
            if(colors.Length == 0)
            {
                return;
            }
            songWasChanged = true;
        }

    }

    private void ColorProgress()
    {
        
        //Debug.Log("Color Progress steps= "+steps+" counts to transition "+ beatCountsToTransition);
        if (beatCountsToTransition == SongChangeColorSteps)
        {
            sprite.color = colors[currentSong % colors.Length];
            beatCountsToTransition = 1;
            CancelInvoke();
        }
        float t = beatCountsToTransition / SongChangeColorSteps; // 0 is a, 1 is b
        // interpolate step beatCountsToTransition between the two colors.
        sprite.color = Color.Lerp(sprite.color, colors[currentSong % colors.Length],t);
        beatCountsToTransition++;
    }

    public override void OnBeat(float jitter_delay, float bps)
    {
        if (debug)
        {
            Debug.Log("Platform colorchange OnBeat was called");
        }
        if (songWasChanged)
        {
            beatCountsToTransition = 1;
            //sprite.color = Color.Lerp(sprite.color, colors[currentSong % colors.Length],1/SongChangeColorSteps);
            InvokeRepeating("ColorProgress", SongSynchonizeVibing.instance.BeatStart+ (SongSynchonizeVibing.instance.BeatLength * delayTransitionByXBeats+1) - Time.time, 1/bps);
            songWasChanged = false;
        }
    }
    
    
    // for Tools/Create Color Change 2D With current Color
    public void OverrideColor_EditorScript(Color color, int songNum = 0)
    {
        colors[songNum % colors.Length] = color;
    }
    
}
