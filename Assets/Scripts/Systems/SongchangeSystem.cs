using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongchangeSystem : MonoBehaviour
{
    public static SongchangeSystem instance;
    private int maxsongs = 2;
    [SerializeField]
    private int currentsong = 0;

    private void Start()
    {
        instance = this;
        Invoke("SongChange", 0.1f);
    }

    public void SongChange()//gets called when user uses Songtree ?
    {
        SongChange(currentsong);
        //todo notify Clock that it's time to change song
        currentsong = (currentsong + 1) % maxsongs;
    }
    public void SongChange(int song)//gets called when user uses Songtree ?
    {
        Debug.Log("Songchanger songchange");
        Background.instance.SetSprites(song);
        SongSynchonizeVibing.instance.RecieveSongChange(song);
        //todo notify Clock that it's time to change song
    }
}
