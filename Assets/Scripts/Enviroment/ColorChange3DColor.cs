using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange3DColor : ColorChange
{
    [SerializeField]
    private Renderer rend;

    public Color[] colors = { new Color(255, 255, 255, 255), new Color(255, 255, 255, 255), new Color(255, 255, 255, 255) };
    
    [SerializeField] private float SongChangeColorSteps = 5;
    [SerializeField] private float delayTransitionByXBeats = 0;
    private int currentSong;
    private bool songWasChanged = false;
    private float beatCountsToTransition = 1;
    
    protected override void showColor(int song)
    {
        if (colors.Length == 0)
        {
            Debug.LogError("color Array Length is zero", this);
        }
        if (!Application.isPlaying)
        {
            rend = GetComponent<Renderer>();
            rend.sharedMaterial.SetColor("_Color", colors[song%colors.Length]);
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

    private Color prevColor;
    private void ColorProgress()
    {
        
        //Debug.Log("Color Progress steps= "+steps+" counts to transition "+ beatCountsToTransition);
        if (beatCountsToTransition == SongChangeColorSteps)
        {
            rend.sharedMaterial.SetColor("_Color", colors[currentSong%colors.Length]);
            beatCountsToTransition = 1;
            CancelInvoke();
        }
        float t = beatCountsToTransition / SongChangeColorSteps; // 0 is a, 1 is b
        // interpolate step beatCountsToTransition between the two colors.
        Color c = Color.Lerp(prevColor, colors[currentSong % colors.Length],t);
        rend.sharedMaterial.SetColor("_Color", c);
        prevColor = c;
        beatCountsToTransition++;
    }

    public override void OnBeat(float jitter_delay, float bps)
    {
        if (songWasChanged)
        {
            beatCountsToTransition = 1;
            //sprite.color = Color.Lerp(sprite.color, colors[currentSong % colors.Length],1/SongChangeColorSteps);
            prevColor = rend.sharedMaterial.GetColor("_Color");
            InvokeRepeating("ColorProgress", SongSynchonizeVibing.Instance.BeatStart+ (SongSynchonizeVibing.Instance.BeatLength * delayTransitionByXBeats+1) - Time.time, 1/bps);
            songWasChanged = false;
        }
    }
}
