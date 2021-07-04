using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange3DTexture : ColorChange
{
    [SerializeField]
    private Renderer rend;
    public Texture[] textures;
    // Start is called before the first frame update

    protected override void showColor(int song)
    {
        if (!enabled)
        {
            return;
        }
        if (rend == null)
        {
            rend = GetComponentInChildren<Renderer>();
        }

        if (rend == null)
        {
            Debug.LogError("CholorChange couldnt find a renderer :(", this);
        }
        rend.sharedMaterial.SetTexture("_MainTex", textures[song % textures.Length]);
    }
}
