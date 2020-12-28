using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreasingSequence : MonoBehaviour
{
    public AudioHelm.Sequencer sequencer;
    public int increase = 3;
    //startingNote ranges from 0 to 127
    public int startingNote = 20;
    // Start is called before the first frame update
    void Start()
    {
        //sequencer.Clear();
        int length = sequencer.length;
        for (int i = 0; i < length; ++i)
        {
            sequencer.AddNote(startingNote + i * increase, i, i + 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
