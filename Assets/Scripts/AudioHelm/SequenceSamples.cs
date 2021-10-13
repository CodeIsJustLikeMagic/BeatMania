using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceSamples : MonoBehaviour
{
    public AudioHelm.Sequencer sequencer;
    public int distance = 16;
    //startingNote ranges from 0 to 127
    public int startingNote = 20;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Sequence Samples", this);
        if (Input.GetKeyDown(KeyCode.E))
        {
            sequencer.Clear();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            sequencer.Clear();
            int length = sequencer.length;
            for (int i = 0; i < length; i += distance)
            {
                sequencer.AddNote(startingNote + 1, i, i + distance / 4);
                sequencer.AddNote(startingNote - 1, i + distance / 2, i + distance / 4 * 3);
            }
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            sequencer.Clear();
            int length = sequencer.length;
            for (int i = 0; i < length; i += distance)
            {
                sequencer.AddNote(startingNote + 1, i, i + distance / 4);
                sequencer.AddNote(startingNote, i + distance / 4, i + distance / 2);
                sequencer.AddNote(startingNote - 2, i + distance / 2, i + distance / 4 * 3);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            sequencer.Clear();
            int length = sequencer.length;
            for (int i = 0; i < length; i += distance)
            {
                sequencer.AddNote(startingNote - 2, i, i + distance / 4);
                sequencer.AddNote(startingNote, i + distance / 4, i + distance / 2);
                sequencer.AddNote(startingNote - 1, i + distance / 2, i + distance / 4 * 3);
            }
        }
        else if (Input.GetKeyDown(KeyCode.U))
        {
            sequencer.Clear();
            int length = sequencer.length;
            for (int i = 0; i < length; i += distance)
            {
                sequencer.AddNote(startingNote - 1, i, i + distance / 8);
                sequencer.AddNote(startingNote + 1, i + distance / 8, i + distance / 2);
                sequencer.AddNote(startingNote - 1, i + distance / 8*5, i + distance / 4 * 3);
            }
        }
    }
}