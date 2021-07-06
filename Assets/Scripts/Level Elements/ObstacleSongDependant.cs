using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSongDependant : VibingEntity
{
    [SerializeField] private bool[] passageBlocked = {true, true, true};
    [SerializeField] private GameObject vis;
    public override void OnSongChange(int song)
    {
        if (vis == null)
        {
            vis = gameObject.transform.GetChild(0).gameObject;
        }

        vis.SetActive(passageBlocked[song % passageBlocked.Length]);
    }

    public override void OnBeat(float jitter_delay, float bps)
    {
        
    }
}
