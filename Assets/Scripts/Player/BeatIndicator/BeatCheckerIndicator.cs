using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//directly show if Beatchecker gives true of false at any time.
public class BeatCheckerIndicator : MonoBehaviour
{
    private Light _light;

    [SerializeField] private SpriteRenderer fakeLight = null;

    [SerializeField] private SpriteRenderer middle = null;


    // Update is called once per frame
    void Update()
    {
        if (BeatChecker.Instance.IsInBeat())
        {
            fakeLight.enabled = true;
            middle.enabled = true;
            //Debug.Log("accepted "+ BeatChecker.instance.IsInBeatMissedBy(Time.time));
        }
        else
        {
            fakeLight.enabled = false;
            middle.enabled = false;
            //Debug.Log("missed "+ BeatChecker.instance.IsInBeatMissedBy(Time.time));
        }
        
        
    }
}
