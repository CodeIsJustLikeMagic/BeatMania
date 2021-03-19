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
    private string[] names = { "yellow", "blue", "white"};
    public void SetNextSprites()
    {
        Debug.Log("songchange called");
        SetSprites(cnt);
        cnt = (cnt + 1) % names.Length;
    }

    public void SetWhiteSprites()
    {
        SetSprites(cnt, "white");
    }
    
    public void SetSprites(int song)
    {
        song = song % names.Length;
        string color = names[song];
        SetSprites(song, color);
    }

    public void SetSprites(int song, string color)
    {
        
        GameObject[] changeSpriteObjects = GameObject.FindGameObjectsWithTag("changeSprite"); // gameObject.GetComponentsInChildren<SpriteRenderer>();

        cnt = song;
        foreach (GameObject obj in changeSpriteObjects)
        {

            SpriteRenderer[] rendererrs = obj.GetComponentsInChildren<SpriteRenderer>();
            foreach(SpriteRenderer rend in rendererrs)
            {
                ColorChange c = rend.gameObject.GetComponent<ColorChange>();
                if(c != null)
                {
                    c.setColor(song);
                }
                else
                {
                    string objectname = rend.gameObject.name;
                    objectname = objectname.Replace("(Clone)", string.Empty);
                    string str = color + objectname;
                    Sprite sprt = Resources.Load<Sprite>("BackgroundTextures/" + str);
                    rend.sprite = sprt;
                }
            }
        }

    }
    

}
