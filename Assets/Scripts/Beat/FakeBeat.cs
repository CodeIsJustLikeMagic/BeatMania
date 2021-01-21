using UnityEngine;
using System.Collections;
using UnityEngine.Events;

//test class that creates Beat Event once every Second
//This class should be replaced by a proper beat system
public class FakeBeat : MonoBehaviour
{
    public static FakeBeat instance;
    
    //public UnityEvent songChange;
    public float beatLength = 1.0f;
    public float beatStart = 0.0f;
    
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;   

    }
    void Start()
    {
        beatLength = 1 / (SongSynchonizeVibing.instance.clock.bpm / 60);
        InvokeRepeating("SongChange", 0, 30.0f); 
        InvokeRepeating("Showbeat", 0, beatLength);
        InvokeRepeating("ShowsecBeat", 0, 1f);
    }

    void Showbeat()
    {
        Debug.Log("Beat triggered ");
    }
    void ShowsecBeat()
    {
        //Debug.Log("1 sec");
    }
    void SongChange()
    {
        beatStart = Time.time;
        //songChange.Invoke();
    }
    
}
