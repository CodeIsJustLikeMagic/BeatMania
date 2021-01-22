using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public static Background instance;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    [SerializeField]
    private int cnt = 1;
    [SerializeField]
    private string[] names = { "yellow", "blue"};
    internal void SetNextSprites()
    {
        Debug.Log("songchange called");
        SetSprites(cnt);
        cnt = (cnt + 1) % names.Length;
    }

    public void SetSprites(int song)
    {
        SpriteRenderer[] rendererrs = gameObject.GetComponentsInChildren<SpriteRenderer>();
        song = song % names.Length;
        string color = names[song];
        cnt = song;
        foreach (SpriteRenderer rend in rendererrs)
        {
            string objectname = rend.gameObject.name;
            objectname = objectname.Replace("(Clone)", string.Empty);
            string str = color+ objectname;
            Sprite sprt = Resources.Load<Sprite>("BackgroundTextures/"+str);
            rend.sprite = sprt;
        }
    }

}
