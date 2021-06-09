using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyParallax : MonoBehaviour
{
    private float length, startpos;
    [SerializeField]
    private GameObject cam;

    [Tooltip("0 for move with camera, 1 stay static as camera moves past")]

    [SerializeField]
    private float parallaxEffect = 1;

    [SerializeField]
    private bool dont_teleport;

    [SerializeField]
    private float flipProb = 0.5f ;

    [SerializeField]
    private float posMulti = 2;

    [SerializeField]
    private float width = 15;
    
    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position.x;
        length = width * posMulti;
        
        cam = GameObject.FindGameObjectWithTag("CinemachineCamera");
    }

    // Update is called once per frame
    void Update()
    {

        float temp = (cam.transform.position.x * (1 - parallaxEffect)); //how far we've moved realtive to camera
        float dist = (cam.transform.position.x * parallaxEffect); // how far we have moved from our startpoint

        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);

        if (dont_teleport)
        {
            return;
        }
        if (temp > startpos + length*2)
        {
            //Debug.Log("moving sprites to the right temp is " + temp);
            startpos += length*3;
            maybeflip();

        }
        else if (temp < startpos - length*2)
        {
            //Debug.Log("moving sprites to the left temp is " + temp);
            startpos -= length*3;
            maybeflip();
        }

    }

    private void maybeflip()
    {
        if (Random.Range(0.0f, 1.0f) < flipProb)
        {
            transform.RotateAround(transform.position, transform.up, 180f);
            return;
        }
    }
}
