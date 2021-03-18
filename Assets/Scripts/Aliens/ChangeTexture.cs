using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTexture : VibingEntity
{
    public Renderer renderer;
    public Texture[] textures;
    // Start is called before the first frame update
    
    void Awake()
    {
        if (renderer is null)
        {
            renderer = GetComponentInChildren<Renderer>();
        }
    }
    public override void OnBeat(float bps)
    {
        return;
    }
    public override void OnSongChange(int song)
    {
        renderer.material.SetTexture("_MainTex", textures[song%textures.Length]);
    }
}
