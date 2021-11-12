using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntennaLight : MonoBehaviour
{
    public Transform AntennaTransform;
    // Start is called before the first frame update
    private void Awake()
    {
        if (AntennaTransform == null)
        {
            Debug.LogError("Antenna Light has no reference to the players Antenna", this);
            gameObject.SetActive(false);
            this.enabled = false;
        }
    }

    private void Update()
    {
        gameObject.transform.position =
            new Vector3(AntennaTransform.position.x, AntennaTransform.position.y, transform.position.z);
    }
}
