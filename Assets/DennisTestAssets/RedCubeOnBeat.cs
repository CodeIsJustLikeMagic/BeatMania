using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RedCubeOnBeat : MonoBehaviour
{
    public Material beatCubeMat;
    private int counter = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void showBeat()
    {
        counter++;
        if(counter%2 == 0)
        {
            beatCubeMat.color = new Color(1f, 0f, 0f);
        }
        else
        {
            beatCubeMat.color = new Color(1f, 1f, 1f);
        }
        
    }
}
