using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//directly show if Beatchecker gives true of false at any time.
public class LightIndicator : MonoBehaviour
{
    private Light light;
    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (BeatChecker.instance.IsInBeat())
        {
            light.enabled = true;
        }
        else
        {
            light.enabled = false;
        }
    }
}
