using UnityEngine;
using System.Collections;
using UnityEngine.Events;

//test class that creates Beat Event once every Second
//This class should be replaced by a proper beat system
public class FakeBeat : MonoBehaviour
{
    public UnityEvent beat;
    public float timeBetweenBeats = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CreateEvent", 0, timeBetweenBeats);
    }

    void CreateEvent()
    {
        Debug.Log("Beat triggered");
        beat.Invoke();
    }
    
}
