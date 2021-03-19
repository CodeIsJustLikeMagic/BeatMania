using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor3DTexture : ColorChange
{
    [SerializeField]
    private Renderer renderer;
    public Texture[] textures;
    // Start is called before the first frame update

    public override void setColor(int song)
    {
        if(renderer is null)
        {
            renderer = GetComponentInChildren<Renderer>();
        }
        renderer.sharedMaterial.SetTexture("_MainTex", textures[song % textures.Length]);
    }
}
