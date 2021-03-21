using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange3DTexture : ColorChange
{
    [SerializeField]
    private Renderer rend;
    public Texture[] textures;
    // Start is called before the first frame update

    public override void setColor(int song)
    {
        rend = GetComponentInChildren<Renderer>();
        rend.sharedMaterial.SetTexture("_MainTex", textures[song % textures.Length]);
    }
}
