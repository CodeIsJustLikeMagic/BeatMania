using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSongDependant : VibingEntity
{
    [SerializeField] private bool[] openPassage = {true, true, true};
    [SerializeField] private GameObject vis;
    public override void OnSongChange(int song)
    {
        if (vis == null)
        {
            Debug.LogError("ObstaceSongDependant does not have a visualize object", this);
            return;
        }

        vis.SetActive(openPassage[song % openPassage.Length]);
    }

    public override void OnBeat(float jitter_delay, float bps)
    {
        
    }
}
