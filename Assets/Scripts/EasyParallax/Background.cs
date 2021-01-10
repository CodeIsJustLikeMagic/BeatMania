using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public static Background instance;
    public SpriteRenderer[] renderers;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    public void Start()
    {
        
        SetSprites("blue");
    }

    public void FindRenderers()
    {
        renderers = gameObject.GetComponentsInChildren<SpriteRenderer>();
    }

    private int cnt = 0;
    private string[] names = { "yellow", "blue"};
    internal void SetNextSprites()
    {
        SetSprites(names[cnt]);
        cnt = (cnt + 1) % names.Length;
    }

    public void SetSprites(string color)
    {
        foreach(SpriteRenderer rend in renderers)
        {
            string str = color+ rend.gameObject.name;
            Sprite sprt = Resources.Load<Sprite>("BackgroundTextures/"+str);
            rend.sprite = sprt;
        }
    }
}
