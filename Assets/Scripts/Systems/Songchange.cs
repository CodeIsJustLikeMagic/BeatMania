using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Songchange : MonoBehaviour
{
    public static Songchange instance;
    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
    }
    public void DoSongChange()
    {
        Background.instance.SetNextSprites();
        //todo trigger clock and synthesiser songchange here
        //also change behaviour of aliens
    }
}
