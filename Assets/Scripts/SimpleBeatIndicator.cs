using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBeatIndicator : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        InvokeRepeating("BeatAnimation", 0, 1.0f);
    }

    float beatStart = 0;
    float beatLength = 0;
    float toleranceRange = 0.5f;//
    public void ListenToSongChange()
    {
        beatStart = FakeBeat.instance.beatStart;
        beatLength = FakeBeat.instance.beatLength;
    }

    void BeatAnimation()
    {
        animator.SetTrigger("beat");
    }

    //on every beat change animation to large
}
