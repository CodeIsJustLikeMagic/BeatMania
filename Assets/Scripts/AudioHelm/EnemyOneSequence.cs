using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOneSequence : MonoBehaviour
{
    AudioHelm.Sequencer sequencer;
    public int offset = 0;
    //startingNote ranges from 0 to 127
    // Start is called before the first frame update
    void Start()
    {
        sequencer = this.GetComponent<AudioHelm.Sequencer>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Enemy One Sequence here", this);
        if (Input.GetKeyDown(KeyCode.E))
        {
            sequencer.Clear();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            sequencer.Clear();
            sequencer.AddNote(70 + offset, 0, 1);
            sequencer.AddNote(82 + offset, 2,4);


        }
    }
}
