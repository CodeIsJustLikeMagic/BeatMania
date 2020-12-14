using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteOnBounce : MonoBehaviour
{
    public AudioHelm.HelmController helmController;
    public int note = 60;
    public float subVolume = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        helmController.SetParameterPercent(AudioHelm.Param.kSubVolume, subVolume);
        helmController.NoteOn(note, 1.0f, 0.5f);
    }
}
